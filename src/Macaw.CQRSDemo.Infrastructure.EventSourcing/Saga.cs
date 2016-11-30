using System;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public abstract class Saga
    {
        public IEventBus EventBus { get; private set; }
        public IEventStore EventStore { get; private set; }


        public Saga(IEventBus eventBus, IEventStore eventStore)
        {
            if (eventBus == null)
            {
                throw new ArgumentNullException(nameof(eventBus));
            }
            if (eventStore == null)
            {
                throw new ArgumentNullException(nameof(eventStore));
            }

            EventBus = eventBus;
            EventStore = eventStore;
        }
    }
}