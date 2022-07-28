using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebexBOT_API.Entities
{
    public class WebexRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Resource { get; set; }
        public string Event { get; set; }
        public string Filter { get; set; }
        public string OrgId { get; set; }
        public string CreatedBy { get; set; }
        public string AppId { get; set; }
        public string OwnedBy { get; set; }
        public string Status { get; set; }
        public string ActorId { get; set; }
        public Data Data { get; set; }
    }
}
