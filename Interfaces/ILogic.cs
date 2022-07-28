using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebexBOT_API.Interfaces
{
    public interface ILogic
    {
        public bool VerifyHash(string PayLoad, string WEBEX_HMACSHA1HASH);
    }
}
