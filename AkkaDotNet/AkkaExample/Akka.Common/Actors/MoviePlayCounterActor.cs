using Akka.Actor;
using Akka.Common.Exceptions;
using Akka.Common.Messages;
using System;
using System.Collections.Generic;

namespace Akka.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
                _moviePlayCounts[message.MovieTitle]++;
            else
                _moviePlayCounts.Add(message.MovieTitle, 1);

            if (_moviePlayCounts[message.MovieTitle] > 3)
                throw new SimulatedCorruptStateException();

            if (message.MovieTitle == "Partial Recoil")
                throw new SimulatedTerribleMovieException();

            ColorConsole.WriteLineMagenta($"MoviePlayCounterActor: '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }


        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineMagenta("MoviePlayCounterActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
