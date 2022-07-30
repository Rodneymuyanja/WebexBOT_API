using System;
using WebexBOT_API.Interfaces;

namespace WebexBOT_API.Entities
{
    public class Message
    {
           //public DateTime Created { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string PersonId { get; set; }
        public string PersonEmail { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }   
}
