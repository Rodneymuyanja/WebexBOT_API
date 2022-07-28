using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebexBOT_API.Interfaces;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace WebexBOT_API.Logic
{
    public class Verification : IVerification
    {

        HMACSHA1 HMACSHA1 = new HMACSHA1();
        readonly IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false,true);

        public byte[] BytesFromString(string PayLoadData)
        {

            
            throw new NotImplementedException();
        }

        public string HashData(string PayLoad)
        {
            IConfigurationRoot root = configurationBuilder.Build();

            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(root["secret"])))
            {
                var payLoadBytes = Encoding.UTF8.GetBytes(PayLoad);
                var hash = hmac.ComputeHash(payLoadBytes);
                return Convert.ToBase64String(hash);
            }

            throw new NotImplementedException();
        }

        public string StringFromBytes(byte[] Bytes)
        {
            throw new NotImplementedException();
        }
    }
}
