namespace Akka.Common.Messages
{
    public class StopMovieMessage
    {
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }
    }
}
