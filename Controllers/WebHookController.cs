using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebexBOT_API.Entities;
using WebexBOT_API.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebexBOT_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController: ControllerBase
    {
        private readonly ILogic _logic;

        public WebHookController(ILogic logic)
        {
            _logic = logic;
        }


        [HttpPost]
        public ActionResult<Response> EventHandler(WebexRequest webexRequest)
        {
            string WEBEX_HMACSHA1HASH = Request.Headers["X-Spark-Signature"];
            string WEBEX_JSONPAYLOAD = JsonSerializer.Serialize(webexRequest);
            bool HASH_VALIDITY = _logic.VerifyHash(WEBEX_JSONPAYLOAD, WEBEX_HMACSHA1HASH);

        }
    }
}
