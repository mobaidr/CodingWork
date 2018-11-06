using Akka.Actor;
using Akka.Common.Messages;
using Akka.Event;
using System;

namespace Akka.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private ILoggingAdapter _logger = Context.GetLogger();

        private int UserId { get; set; }

        public UserActor(int userId)
        {
            UserId = userId;

            Stopped();
        }


        private void Playing()
        {
            Receive<PlayMovieMessage>(msg =>
            {
                _logger.Warning($"User {UserId} cannot start playing another movie before stopping existing one");
            });
            Receive<StopMovieMessage>(msg => StopPlayingCurrentMovie());

            _logger.Info($"UserActor {UserId} has now become playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg => StartPlayingMovie(msg.MovieTitle));
            Receive<StopMovieMessage>(msg =>
            {
                _logger.Warning($"User {UserId} cannot stop if nothing is playing");
            });


            _logger.Info($"UserActor {UserId} has now become stopped");
        }

        private void StopPlayingCurrentMovie()
        {
            _logger.Info($"User {UserId} has stopped watching {_currentlyWatching}");

            _currentlyWatching = null;

            Become(Stopped);
        }


        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            _logger.Info($"User {UserId} is current watching {_currentlyWatching}");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(movieTitle));

            Become(Playing);
        }

        protected override void PreStart()
        {
            _logger.Debug($"UserActor {UserId} Prestart");
        }

        protected override void PostStop()
        {
            _logger.Debug($"UserActor {UserId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug($"UserActor {UserId} PreRestart because : {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("UserActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
    }
}
