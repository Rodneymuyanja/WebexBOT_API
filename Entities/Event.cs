using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebexBOT_API.Interfaces;

namespace WebexBOT_API.Entities
{
    public class Event: IEvent
    {
        public Event(string name, string description)
        {
            Name = name;
            ReceivedAt = DateTime.Now;
            Description = description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string Description { get; set; }
    }
}
