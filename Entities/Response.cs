using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebexBOT_API.Entities
{
    public class Response
    {
       
        public string RoomId { get; set; }
        public string ParentId { get; set; }
        public string ToPersonId { get; set; }
        public string ToPersonEmail { get; set; }
        public string Text { get; set; }
        public string Markdown { get; set; }    
        public string Files { get; set; }
        public string[] Attachment { get; set; }
    }
}
