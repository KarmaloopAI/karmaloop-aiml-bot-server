using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KarmaloopAIMLBotServer.API
{
    [RoutePrefix("api/ChatUi")]
    public class ChatUiController : ApiController
    {
        
        public IHttpActionResult Get(string fileName)
        {

            return Ok(fileName);
        }
    }
}
