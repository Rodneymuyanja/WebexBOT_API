using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebexBOT_API.Entities;
using WebexBOT_API.Interfaces;

namespace WebexBOT_API.Logic
{
    public class Logic : ILogic
    {
        private readonly IVerification _HashVerification;
        private readonly IEvent _CreateEvent;
        private readonly IEvent _DeleteEvent;

        List<IEvent> events = new List<IEvent>();
        private Dictionary<string, List<IEvent>> Resources = new Dictionary<string, List<IEvent>>();
        public Logic(IVerification verification)
        {
            _HashVerification = verification;
            _CreateEvent = new Event("Create", "This indicates a new message has been added into the space");
            _DeleteEvent = new Event("Delete", "This indicates a message has been deleted from the space");
            events.Add(_CreateEvent);
            events.Add(_DeleteEvent);
            Resources.Add("messages",events);
        }

    
        public void HandleRequest(WebexRequest webexRequest)
        {
            string resource = webexRequest.Resource;
            Event k = null;

            if (string.IsNullOrEmpty(resource))
            {
                return;
            }
            else
            {
                if(Resources.TryGetValue(resource, out List<IEvent> events))
                {
                    k = (Event)(from e in events
                              where e.Name == webexRequest.Name
                              select e);
                }
            }


            if (k != null)
            {
                HandleEvent(k);
            }
        }

        public bool VerifyHash(string PayLoad, string WEBEX_HASH)
        {
            string GENERATEDHASH = _HashVerification.HashData(PayLoad);
            if (GENERATEDHASH == WEBEX_HASH)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void HandleEvent(Event @event)
        {

        }

    }
}
