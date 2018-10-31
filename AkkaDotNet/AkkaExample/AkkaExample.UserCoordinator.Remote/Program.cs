using Akka.Actor;
using Akka.Common;

namespace AkkaExample.UserCoordinator.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.WhenTerminated.Wait();
        }
    }
}
