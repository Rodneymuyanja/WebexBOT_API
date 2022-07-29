using System;
using WebexBOT_API.Interfaces;

namespace WebexBOT_API.Entities
{
    public class Message : IMessage
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RoomId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersonId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PersonEmail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Created { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
