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
using Newtonsoft.Json;

namespace WebexBOT_API.Logic
{
    public class Logic : ILogic
    {
        private  Verification _HashVerification;
        private  Event _CreateEvent;
        private  Event _DeleteEvent;
        private Data _data;
        readonly IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", false, true);


        List<Event> events = new List<Event>();
        private Dictionary<string, List<Event>> Resources = new Dictionary<string, List<Event>>();
        public Logic(Verification verification)
        {
            _HashVerification = verification;
            _CreateEvent = new Event("created", "This indicates a new message has been added into the space");
            _DeleteEvent = new Event("deleted", "This indicates a message has been deleted from the space");
            events.Add(_CreateEvent);
            events.Add(_DeleteEvent);
            Resources.Add("messages",events);
        }

    
        public void HandleRequest(WebexRequest webexRequest)
        {
            string resource = webexRequest.Resource;
            Event k = null;
            _data = webexRequest.Data;
            dynamic v = null;

            if (string.IsNullOrEmpty(resource))
            {
                return;
            }
            else
            {
                if(Resources.TryGetValue(resource, out List<Event> events))
                {
                    v  = from e in events
                              where e.Name == webexRequest.Event
                              select e;
                }
            }


            foreach (Event item in v)
            {
                HandleEvent(item);
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
            Response BOT_response = new Response();

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
                    message = JsonConvert.DeserializeObject<Message>(json);
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
                string messagePayLoad = JsonConvert.SerializeObject(BOT_response);

                var request = new HttpRequestMessage(HttpMethod.Post, messageURL);
                request.Content = new StringContent(messagePayLoad, Encoding.UTF8, "application/json");
                //request.Headers.Add("Bearer", root["Bearer"]);

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
