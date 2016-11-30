using Macaw.CQRSDemo.Infrastructure.EventSourcing;
using Macaw.CQRSDemo.Infrastructure.Repositories;
using Macaw.CQRSDemo.MatchPlay.CommandStack.Messages.Commands;

namespace Macaw.CQRSDemo.MatchPlay.CommandStack.Sagas
{
    public class ResetDbSaga : Saga,
        IStartWithMessage<ResetDbCommand>
    {
        private readonly AdminRepository _repository;

        public ResetDbSaga(AdminRepository repository, IEventBus eventBus, IEventStore eventStore) : base(eventBus, eventStore)
        {
            _repository = repository;
        }

        public void Handle(ResetDbCommand message)
        {
            // 1. Clear the relational DB used to track scheduled matches
            _repository.ResetDb();

            // 2. Places two commands to create new matches against MatchSaga
            var cmd1 = new CreateMatchCommand("WP0001", "Frogs", "Sharks");
            EventBus.Send(cmd1);
            var cmd2 = new CreateMatchCommand("WP0002", "Sharks", "Eels");
            EventBus.Send(cmd2);
        }
    }
}