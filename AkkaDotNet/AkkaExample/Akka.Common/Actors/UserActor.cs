using Akka.Actor;
using Akka.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating UserActor");

            ColorConsole.WriteCyanLine("Setting initial behaviour to stopped");
            Stopped();
        }


        private void Playing()
        {
            Receive<PlayMovieMessage>(msg => ColorConsole.WriteRedLine("Error: Cannot start playing another movie before stoppping existing one"));
            Receive<StopMovieMessage>(msg => StopPlayingCurrentMovie());

            ColorConsole.WriteCyanLine("UserActor has become Playing.");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg => StartPlayingMovie(msg.MovieTitle));
            Receive<StopMovieMessage>(msg => ColorConsole.WriteRedLine("Error: Cannot stop if nothing is playing."));


            ColorConsole.WriteCyanLine("UserActor has now become stopped");
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

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("UserActor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("UserActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("UserActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
    }
}
