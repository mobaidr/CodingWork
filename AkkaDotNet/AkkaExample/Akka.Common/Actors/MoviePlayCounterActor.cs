using Akka.Actor;
using Akka.Common.Exceptions;
using Akka.Common.Messages;
using Akka.Event;
using System;
using System.Collections.Generic;

namespace Akka.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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

            _logger.Info($"MoviePlayCounterActor: '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }


        #region Lifecycle Hooks
        protected override void PreStart()
        {
            _logger.Debug("MoviePlayCounterActor Prestart");
        }

        protected override void PostStop()
        {
            _logger.Debug("MoviePlayCounterActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("MoviePlayCounterActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("MoviePlayCounterActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
