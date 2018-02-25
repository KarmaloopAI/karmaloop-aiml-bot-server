using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmaloopAIMLBotServer.API
{
    /// <summary>
    /// A simple POCO to represent an incoming Conversation Request. Created a model so that we can extend it in the future.
    /// </summary>
    public class ConversationRequest
    {
        /// <summary>
        /// The input sentence.
        /// </summary>
        public string Sentence { get; set; }
    }
}
