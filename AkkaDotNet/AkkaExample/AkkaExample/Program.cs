using System;
using Akka.Actor;
using AkkaExample.Actors;
using AkkaExample.Messages;

namespace AkkaExample
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props playbackActorProps = Props.Create<PlaybackActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PLaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.Net The Movie", 1234));
            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();
        }
    }
}
