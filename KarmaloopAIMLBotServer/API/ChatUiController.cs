using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KarmaloopAIMLBotServer.API
{
    [RoutePrefix("api/ChatUi")]
    public class ChatUiController : ApiController
    {
        /// <summary>
        /// ChatUi file server wrapped within an API call. This UI is primarily for quick and dirty demo purposes but
        /// can also be used to embed a chatbox within an existing web app.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string fileName)
        {
            string www = "www";
            string filePath = string.Empty;
            string fileContents = string.Empty;

            if (ConfigurationManager.AppSettings["wwwFolder"] != null)
                www = ConfigurationManager.AppSettings["wwwFolder"].ToString().Trim();

            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, www, fileName);
            if(File.Exists(filePath))
            {
                fileContents = File.ReadAllText(filePath);
            }

            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(fileContents);

            if (fileName.EndsWith(".html") || fileName.EndsWith(".htm"))
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            else if (fileName.EndsWith(".css"))
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/css");
            else if (fileName.EndsWith(".js"))
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/javascript");
            else if (fileName.EndsWith(".json"))
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            else
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}
