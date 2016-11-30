using System;

namespace Macaw.CQRSDemo.Infrastructure.Repositories.DataModels
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EventType { get; set; }
        public string PartitionKey { get; set; }
        public string Payload { get; set; }
    }
}