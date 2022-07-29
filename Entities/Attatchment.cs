using System;
using Microsoft.Extensions.Configuration;

namespace WebexBOT_API.Entities
{
    public class Attatchment
    {

        private readonly IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile("Entities/CardPayload.json", false, true);

        public string Username { get; set; }   

        public string PayLoad()
        {
            IConfigurationRoot root = configurationBuilder.Build();
            return root["payload"];
        }
    
    }
}
