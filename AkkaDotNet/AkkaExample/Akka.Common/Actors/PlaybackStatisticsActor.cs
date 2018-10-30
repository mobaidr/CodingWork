using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {

        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }


        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite("PlaybackStatisticsActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
