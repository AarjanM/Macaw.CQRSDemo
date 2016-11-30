using System;

namespace Macaw.CQRSDemo.Infrastructure.EventSourcing
{
    public abstract class EventBase : Message
    {
        public EventType Code { get; protected set; }
        public DateTime TimeStamp { get; private set; }
        public object EventArgs { get; protected set; }


        protected EventBase()
        {
            TimeStamp = DateTime.Now;
            Code = EventType.Unknown;
        }
    }
}