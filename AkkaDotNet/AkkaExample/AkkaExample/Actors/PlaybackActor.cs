using System;
using Akka.Actor;
using AkkaExample.Messages;

namespace AkkaExample.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Receive<PlayMovieMessage>(m=>PlayMovieMessageHandler(m), m => m.UserId>123);
        }

        private void PlayMovieMessageHandler(PlayMovieMessage msg)
        {
            Console.WriteLine($"Received movie title {msg.MovieTitle}");
            Console.WriteLine($"User ID received {msg.UserId}");
        }
    }
}
