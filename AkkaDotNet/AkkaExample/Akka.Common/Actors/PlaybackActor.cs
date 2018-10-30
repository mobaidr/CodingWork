using Akka.Actor;
using Akka.Common.Messages;
using System;

namespace Akka.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Receive<PlayMovieMessage>(m => PlayMovieMessageHandler(m));
        }

        private void PlayMovieMessageHandler(PlayMovieMessage msg)
        {
            ColorConsole.WriteLineYellow($"PlayMovieMessage '{msg.MovieTitle}' for user {msg.UserId}");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("Playback actor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("Playback Actor PostStop");
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
    }
}
