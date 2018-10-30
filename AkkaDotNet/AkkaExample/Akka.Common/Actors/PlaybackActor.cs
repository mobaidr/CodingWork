using Akka.Actor;
using System;

namespace Akka.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");

        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("Playback actor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("Playbackactor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("Playbackactor Post Restart because : " + reason);
            base.PostRestart(reason);
        } 
        #endregion
    }
}
