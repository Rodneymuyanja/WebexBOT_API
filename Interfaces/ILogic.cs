using System;
using System.Collections.Generic;
using System.Linq;
using WebexBOT_API.Entities;
using System.Threading.Tasks;

namespace WebexBOT_API.Interfaces
{
    public interface ILogic
    {
        public bool VerifyHash(string PayLoad, string WEBEX_HMACSHA1HASH);
        public void HandleRequest(WebexRequest webexRequest);
        public void HandleEvent(Event @event);
    }
}
