using System;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public abstract class Handler
    {
        public IEventStore EventStore { get; private set; }


        public Handler(IEventStore eventStore)
        {
            if (eventStore == null)
            {
                throw new ArgumentNullException(nameof(eventStore));
            }

            EventStore = eventStore;
        }
    }
}