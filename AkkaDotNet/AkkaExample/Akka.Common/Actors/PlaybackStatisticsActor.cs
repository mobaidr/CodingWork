using Akka.Actor;
using Akka.Common.Exceptions;
using Akka.Event;
using System;

namespace Akka.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is SimulatedCorruptStateException)
                    {
                        _logger.Error(exception, "PlaybackStatisticsActor supervisory stratergy restarting child due to SimulatedCorruptStateException");

                        return Directive.Restart;
                    }

                    if (exception is SimulatedTerribleMovieException)
                    {
                        _logger.Warning("PlaybackStatisticsActor supervisory stratergy resume child due to SimulatedTerribleMovieException");

                        return Directive.Resume;
                    }

                    _logger.Error(exception, "PlaybackStatisticsActor supervisory stratergy restarting child due to unexpected exception");

                    return Directive.Restart;
                }

                );
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            _logger.Debug($"UserActor Prestart");
        }

        protected override void PostStop()
        {
            _logger.Debug($"UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("PlaybackStatisticsActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("PlaybackStatisticsActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
