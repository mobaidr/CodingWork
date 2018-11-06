using Akka.Actor;
using Akka.Event;
using System;

namespace Akka.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");

        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            _logger.Debug("Playback actor Prestart");
        }

        protected override void PostStop()
        {
            _logger.Debug("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("Playbackactor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("Playbackactor Post Restart because : " + reason);
            base.PostRestart(reason);
        } 
        #endregion
    }
}
