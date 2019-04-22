using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace KarmaloopAIMLBotServer.API
{
    public class ConversationController : ApiController
    {
        /// <summary>
        /// Lets you converse with the Bot. This is the most important API call.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public IHttpActionResult Post(string userId, [FromBody]ConversationRequest request)
        {
            
            string ques = request.Sentence;
            string klServer = "localhost";
            int klPort = 8888;
            string eofId = "$";
            ConversationResponseModel response = null;

            if (ConfigurationManager.AppSettings["apiTargetServer"] != null)
                klServer = ConfigurationManager.AppSettings["apiTargetServer"].ToString();

            if (ConfigurationManager.AppSettings["apiTargetPort"] != null)
                klPort = Convert.ToInt32(ConfigurationManager.AppSettings["apiTargetPort"]);

            if (ConfigurationManager.AppSettings["EOFIdentifier"] != null)
                eofId = ConfigurationManager.AppSettings["EOFIdentifier"].ToString();

            if (!string.IsNullOrEmpty(ques))
            {
                string sendString = string.Concat("{"
                    , "\"question\": \"", ques, "\","
                    , "\"userid\": \"", userId, "\""
                    , "}"
                    , eofId);
                byte[] bytes = Encoding.UTF8.GetBytes(sendString.ToString());
                byte[] receiveBuffer = new byte[10000];
                string bufferString = String.Empty;

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(klServer, klPort);
                client.Send(bytes);
                
                client.Receive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None);
                bufferString = Encoding.GetEncoding(949).GetString(receiveBuffer).Trim().Replace("\0", "");

                response = new ConversationResponseModel(bufferString, userId);

                try
                {
                    client.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    client.Close();
                }
            }
            else
            {
                response = new ConversationResponseModel("My brain had trouble processing this request :-(", userId);
            }

            return Ok<ConversationResponseModel>(response);
        }

        /// <summary>
        /// This lets you call some auxilliary functions for the user like creating a new user id. This will be extended for more functions.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string userId)
        {
            ResponseModel response = new ResponseModel(CodeType.SUCCESS, StateType.NORMAL, string.Empty);
            if (!string.IsNullOrEmpty(userId) && userId.ToLower() == AuxilliaryUserFunctions.NEWUSER.ToLower())
            {
                response.UserID = Guid.NewGuid().ToString();
            }
            else
            {
                response.Code = CodeType.ERROR;
                response.State = StateType.UNHANDLED;
            }

            return Ok(response);
        }
    }
}
