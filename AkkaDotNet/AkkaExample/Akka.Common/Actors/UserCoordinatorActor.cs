using Akka.Actor;
using Akka.Common.Messages;
using Akka.Event;
using System;
using System.Collections.Generic;

namespace Akka.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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

                _logger.Info($"UserCoordinatorActor created new child  UserActor for {userId} (Total Users : {_users.Count})");
            }
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            _logger.Debug("UserCoordinatorActor Prestart");
        }

        protected override void PostStop()
        {
            _logger.Debug("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("UserCoordinatorActor PreRestart because : " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("UserCoordinatorActor Post Restart because : " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
