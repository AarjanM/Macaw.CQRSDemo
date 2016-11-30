using System.Collections.Generic;
using System.Reflection;
using Macaw.CQRSDemo.Infrastructure.Repositories;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;
using Newtonsoft.Json.Linq;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly EventRepository _eventRepository;

        public SqlEventStore(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> All(string partionKey)
        {
            return _eventRepository.All(partionKey);
        }

        public void Save<T>(T @event) where T : EventBase
        {
            _eventRepository.Store(new Event
            {
                EventType = @event.Code.ToString(),
                PartitionKey = @event.PartionKey,
                TimeStamp = @event.TimeStamp,
                Payload = @event.EventArgs != null ? JObject.FromObject(@event.EventArgs).ToString() : null
            });
        }
    }
}