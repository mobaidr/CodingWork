using Akka.Actor;
using Akka.Common.Messages;
using System;
using System.Collections.Generic;

namespace Akka.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildIfNotExists(message.UserId);

                IActorRef childActor = _users[message.UserId];

                childActor.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildIfNotExists(message.UserId);

                IActorRef childActor = _users[message.UserId];

                childActor.Tell(message);
            });


        }

        private void CreateChildIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newchildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "UserId" + userId);

                _users.Add(userId, newchildActorRef);

                ColorConsole.WriteCyanLine($"UserCoordinatorActor created new child  UserActor for {userId} (Total Users : {_users.Count})");
            }
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("UserCoordinatorActor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("UserCoordinatorActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("UserCoordinatorActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
