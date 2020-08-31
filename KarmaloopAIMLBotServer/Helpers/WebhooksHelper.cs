using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using KarmaloopAIMLBot;
using Newtonsoft.Json;

namespace KarmaloopAIMLBotServer.Helpers
{
    /// <summary>
    /// Helper class for Webhooks
    /// </summary>
    public static class WebhooksHelper
    {
        /// <summary>
        /// A simple pre response hook
        /// </summary>
        /// <param name="utterance"></param>
        /// <param name="user"></param>
        public static void PreResponseHook(string utterance, BotUser user)
        {

        }

        /// <summary>
        /// A simple post response formulation hook
        /// </summary>
        /// <param name="utterance"></param>
        /// <param name="reply"></param>
        /// <param name="user"></param>
        public static void PostResponseHook(string utterance, string reply, BotUser user)
        {
            var postHookUrl = ConfigurationManager.AppSettings["postHookUrl"];

            if (postHookUrl != null && postHookUrl.ToLower().StartsWith("http"))
            {
                var botId = ConfigurationManager.AppSettings["botID"];

                if (string.IsNullOrEmpty(botId))
                {
                    Console.WriteLine("You have configured a Post Hook URL but have not set a Bot ID.");
                    return;
                }

                var odata = new
                {
                    botId = botId,
                    botReply = reply,
                    userReply = utterance,
                    userId = user.UserID.ToString()
                };

                string postData = JsonConvert.SerializeObject(odata);

                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(postHookUrl);
                myHttpWebRequest.Method = "POST";

                byte[] data = Encoding.ASCII.GetBytes(postData);

                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.ContentLength = data.Length;

                Stream requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                try
                {
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    Stream responseStream = myHttpWebResponse.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                    string pageContent = myStreamReader.ReadToEnd();

                    myStreamReader.Close();
                    responseStream.Close();
                    myHttpWebResponse.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception trying to call the Post Response Hook: " + ex.Message);
                }
            }
        }
    }
}
