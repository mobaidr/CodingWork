using Akka.Actor;
using Akka.Common.Exceptions;
using System;

namespace Akka.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {

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
                        return Directive.Restart;

                    if (exception is SimulatedTerribleMovieException)
                        return Directive.Resume;

                    return Directive.Restart;
                }

                );
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
