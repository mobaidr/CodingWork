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

            Props playbackActorProps = Props.Create<UserActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage(Codenan The Destroyer)");
            playbackActorRef.Tell(new PlayMovieMessage("Codenan The Destroyer", 58));

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage(Boolean Lies)");
            playbackActorRef.Tell(new PlayMovieMessage("Boolena Lies", 77));
            

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            playbackActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            playbackActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();

            MovieStreamingActorSystem.Terminate().Wait();

            ColorConsole.WriteGreenLine("Actor system terminated");

            Console.ReadKey();
        }
    }
}
