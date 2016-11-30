using System.Collections.Generic;
using Macaw.CQRSDemo.Infrastructure.Repositories.DataModels;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public interface IEventStore
    {
        IEnumerable<Event> All(string partionKey);

        void Save<T>(T @event) where T : EventBase;
    }
}