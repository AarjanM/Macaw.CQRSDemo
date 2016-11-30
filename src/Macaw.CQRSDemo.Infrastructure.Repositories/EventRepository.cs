using System;
using System.Linq;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;

namespace Macaw.CQRSDemo.Infrastructure.Repositories
{
    public class EventRepository
    {
        private readonly DemoContext _demoContext;

        public EventRepository(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public void Store(Event @event)
        {
            @event.TimeStamp = DateTime.UtcNow;
            _demoContext.Events.Add(@event);
            _demoContext.SaveChanges();
        }

        public void RemoveMostRecent(string partitionKey)
        {
            var last = _demoContext.Events.Where(e => e.PartitionKey == partitionKey).OrderByDescending(e => e.EventId).FirstOrDefault();
            if (last == null)
            {
                return;
            }

            _demoContext.Events.Remove(last);
            _demoContext.SaveChanges();
        }

        public IQueryable<Event> All(string partitionKey)
        {
            return _demoContext.Events.Where(e => e.PartitionKey == partitionKey);
        }
    }
}