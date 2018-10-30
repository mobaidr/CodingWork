using Akka.Actor;
using Akka.Common.Messages;
using System.Collections.Generic;

namespace Akka.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
                _moviePlayCounts[message.MovieTitle]++;
            else
                _moviePlayCounts.Add(message.MovieTitle, 1);

            ColorConsole.WriteLineMagenta($"MoviePlayCounterActor: '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }
    }
}
