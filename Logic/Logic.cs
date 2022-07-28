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
        public Logic(IVerification verification)
        {
            _HashVerification = verification;
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
    }
}
