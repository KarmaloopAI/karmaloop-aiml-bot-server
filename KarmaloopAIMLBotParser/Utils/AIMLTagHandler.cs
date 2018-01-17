using System;
using System.Text;
using System.Xml;

namespace KarmaloopAIMLBot.Utils 
{
    /// <summary>
    /// The template for all classes that handle the AIML tags found within template nodes of a
    /// category.
    /// </summary>
    abstract public class AIMLTagHandler : TextTransformer
    { 
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="query">The query that originated this node</param>
        /// <param name="request">The request itself</param>
        /// <param name="result">The result to be passed back to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public AIMLTagHandler   (   KarmaloopAIMLBot.AIMLBot bot, 
                                    KarmaloopAIMLBot.BotUser user, 
                                    KarmaloopAIMLBot.Utils.SubQuery query,
                                    KarmaloopAIMLBot.Request request, 
                                    KarmaloopAIMLBot.Result result, 
                                    XmlNode templateNode) :base(bot,templateNode.OuterXml)
        {
            this.user = user;
            this.query = query;
            this.request = request;
            this.result = result;
            this.templateNode = templateNode;
            this.templateNode.Attributes.RemoveNamedItem("xmlns");
        }

        /// <summary>
        /// Default ctor to use when late binding
        /// </summary>
        public AIMLTagHandler()
        {
        }

        /// <summary>
        /// A flag to denote if inner tags are to be processed recursively before processing this tag
        /// </summary>
        public bool isRecursive = true;

        /// <summary>
        /// A representation of the user who made the request
        /// </summary>
        public KarmaloopAIMLBot.BotUser user;

        /// <summary>
        /// The query that produced this node containing the wildcard matches
        /// </summary>
        public KarmaloopAIMLBot.Utils.SubQuery query;

        /// <summary>
        /// A representation of the input into the bot made by the user
        /// </summary>
        public KarmaloopAIMLBot.Request request;

        /// <summary>
        /// A representation of the result to be returned to the user
        /// </summary>
        public KarmaloopAIMLBot.Result result;

        /// <summary>
        /// The template node to be processed by the class
        /// </summary>
        public XmlNode templateNode;

        #region Helper methods

        /// <summary>
        /// Helper method that turns the passed string into an XML node
        /// </summary>
        /// <param name="outerXML">the string to XMLize</param>
        /// <returns>The XML node</returns>
        public static XmlNode getNode(string outerXML)
        {
            XmlDocument temp = new XmlDocument();
            temp.LoadXml(outerXML);
            return temp.FirstChild;
        }
        #endregion
    }
}
