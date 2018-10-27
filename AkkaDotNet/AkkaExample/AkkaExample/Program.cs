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
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.Net The Movie", 1234));
            playbackActorRef.Tell(new PlayMovieMessage("Partial Recalls",99));
            playbackActorRef.Tell(new PlayMovieMessage("Boolena Lies", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Codenan The Destroyer", 58));

            playbackActorRef.Tell(PoisonPill.Instance);

            Console.ReadKey();


            MovieStreamingActorSystem.Terminate().Wait();

            ColorConsole.WriteGreenLine("Actor system terminated");

            Console.ReadKey();
        }
    }
}
