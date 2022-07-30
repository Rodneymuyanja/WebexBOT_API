using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebexBOT_API.Entities;
using WebexBOT_API.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IO;

namespace WebexBOT_API.Logic
{
    public class Logic : ILogic
    {
        private readonly IVerification _HashVerification;
        private readonly IEvent _CreateEvent;
        private readonly IEvent _DeleteEvent;
        private Data _data;
        readonly IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false, true);


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
            _data = webexRequest.Data;

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


        public void HandleEvent(Event k)
        {
            if (k.Name == "created")
            {
                HandleCreateEvent();
            }
        }

        private void HandleCreateEvent()
        {
            ///we need to pick the message details from webex
            ///
            string messageId = _data.Id;
            Message message = null;
            Response BOT_response = null;

            IConfigurationRoot root = configurationBuilder.Build();

            string WEBEX_ENDPOINT = "https://webexapis.com/v1/messages/" + messageId;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(WEBEX_ENDPOINT);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", root["Bearer"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage response = client.GetAsync(WEBEX_ENDPOINT).Result)
            {
                using (HttpContent content = response.Content)
                {
                    var json = content.ReadAsStringAsync().Result;
                    message = JsonSerializer.Deserialize<Message>(json);
                }
            }

            if (message != null)
            {
                BOT_response.Text = "Hi, not cool that you don't greet";
                string messageURL = "https://webexapis.com/v1/messages";
                if (message.Text.Contains("hi"))
                {
                    BOT_response.Text = "Hi, i received your message";
                }


                BOT_response.RoomId = message.RoomId;
                BOT_response.ParentId = message.Id;
                BOT_response.ToPersonEmail = message.PersonEmail;

                // make post request right here
                string messagePayLoad = JsonSerializer.Serialize(BOT_response);

                var request = new HttpRequestMessage(HttpMethod.Post, messageURL);
                request.Content = new StringContent(messagePayLoad, Encoding.UTF8, "application/json");
                request.Headers.Add("Bearer", root["Bearer"]);

                using (var ResponseOnMessageSend = client.SendAsync(request))
                {
                    using (HttpResponseMessage result = ResponseOnMessageSend.Result)
                    {
                        using(var content = result.Content.ReadAsStringAsync())
                        {
                            var reader = new StreamReader(content.Result);
                            string strContent = reader.ReadToEnd();

                        }
                    }
                }
                

            }
        }
    }
}
