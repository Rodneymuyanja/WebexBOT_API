using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebexBOT_API.Interfaces
{
    public interface IVerification
    {
        public string HashData(string PayLoad);
        public byte[] BytesFromString(string PayLoadData);
        public string StringFromBytes(byte[] Bytes);
    }
}
