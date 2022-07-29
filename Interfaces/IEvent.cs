using System;

namespace WebexBOT_API.Interfaces
{
    public interface IEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string Description { get; set; }

        
    }
}
