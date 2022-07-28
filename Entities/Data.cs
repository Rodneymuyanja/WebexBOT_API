using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebexBOT_API.Entities
{
    public class Data
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string PersonId { get; set; }
        public string PersonEmail { get; set; }
        public DateTime Created { get; set; }
    }
}
