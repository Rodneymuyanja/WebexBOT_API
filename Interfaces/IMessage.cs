using System;

namespace WebexBOT_API.Interfaces
{
    public interface IMessage
    {
        public string Id { get; set; }
        public string RoomId { get; set; }  
        public string PersonId { get; set; }    
        public string PersonEmail { get; set; } 
        public string Text { get; set; }
        public DateTime Created { get; set; }

    }
}
