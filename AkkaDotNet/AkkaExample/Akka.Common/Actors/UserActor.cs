using Akka.Actor;
using Akka.Common.Messages;
using System;

namespace Akka.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private int UserId { get;  set; }

        public UserActor(int userId)
        {
            UserId = userId;

            Stopped();
        }


        private void Playing()
        {
            Receive<PlayMovieMessage>(msg => ColorConsole.WriteRedLine("Error: Cannot start playing another movie before stoppping existing one"));
            Receive<StopMovieMessage>(msg => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow("UserActor has become Playing.");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg => StartPlayingMovie(msg.MovieTitle));
            Receive<StopMovieMessage>(msg => ColorConsole.WriteRedLine("Error: Cannot stop if nothing is playing."));


            ColorConsole.WriteLineYellow("UserActor has now become stopped");
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching {_currentlyWatching}");

            _currentlyWatching = null;

            Become(Stopped);
        }


        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            ColorConsole.WriteLineYellow($"User is current watching {_currentlyWatching}");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(movieTitle));

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow("UserActor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor {UserId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow("UserActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow("UserActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
    }
}
