using System;
using System.Xml;
using System.Text;

namespace KarmaloopAIMLBot.AIMLTagHandlers
{
    /// <summary>
    /// The formal element tells the AIML interpreter to render the contents of the element 
    /// such that the first letter of each word is in uppercase, as defined (if defined) by 
    /// the locale indicated by the specified language (if specified). This is similar to methods 
    /// that are sometimes called "Title Case". 
    /// 
    /// If no character in this string has a different uppercase version, based on the Unicode 
    /// standard, then the original string is returned.
    /// </summary>
    public class formal : KarmaloopAIMLBot.Utils.AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public formal(KarmaloopAIMLBot.AIMLBot bot,
                        KarmaloopAIMLBot.BotUser user,
                        KarmaloopAIMLBot.Utils.SubQuery query,
                        KarmaloopAIMLBot.Request request,
                        KarmaloopAIMLBot.Result result,
                        XmlNode templateNode)
            : base(bot, user, query, request, result, templateNode)
        {
        }

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "formal")
            {
                StringBuilder result = new StringBuilder();
                if (this.templateNode.InnerText.Length > 0)
                {
                    string[] words = this.templateNode.InnerText.ToLower().Split();
                    foreach (string word in words)
                    {
                        string newWord = word.Substring(0, 1);
                        newWord = newWord.ToUpper();
                        if (word.Length > 1)
                        {
                            newWord += word.Substring(1);
                        }
                        result.Append(newWord + " ");
                    }
                }
                return result.ToString().Trim();
            }
            return string.Empty;
        }
    }
}
