using System;
using System.Collections.Generic;
using System.Text;

namespace KarmaloopAIMLBot
{
    /// <summary>
    /// Encapsulates information about the result of a request to the bot
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The bot that is providing the answer
        /// </summary>
        public AIMLBot bot;

        /// <summary>
        /// The user for whom this is a result
        /// </summary>
        public BotUser user;

        /// <summary>
        /// The request from the user
        /// </summary>
        public Request request;

        /// <summary>
        /// The raw input from the user
        /// </summary>
        public string RawInput
        {
            get
            {
                return this.request.rawInput;
            }
        }

        /// <summary>
        /// The normalized sentence(s) (paths) fed into the graphmaster
        /// </summary>
        public List<string> NormalizedPaths = new List<string>();

        /// <summary>
        /// The amount of time the request took to process
        /// </summary>
        public TimeSpan Duration;

        /// <summary>
        /// The result from the bot with logging and checking
        /// </summary>
        public string Output
        {
            get
            {
                if (OutputSentences.Count > 0)
                {
                    return this.RawOutput;
                }
                else
                {
                    if (this.request.hasTimedOut)
                    {
                        return this.bot.TimeOutMessage;
                    }
                    else
                    {
                        StringBuilder paths = new StringBuilder();
                        foreach (string pattern in this.NormalizedPaths)
                        {
                            paths.Append(pattern + Environment.NewLine);
                        }
                        this.bot.writeToLog("The bot could not find any response for the input: " + this.RawInput + " with the path(s): " + Environment.NewLine + paths.ToString() + " from the user with an id: " + this.user.UserID);
                        return string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the raw sentences without any logging 
        /// </summary>
        public string RawOutput
        {
            get
            {
                StringBuilder result = new StringBuilder();
                foreach (string sentence in OutputSentences)
                {
                    string sentenceForOutput = sentence.Trim();
                    if (!this.checkEndsAsSentence(sentenceForOutput))
                    {
                        sentenceForOutput += ".";
                    }
                    result.Append(sentenceForOutput + " ");
                }
                return result.ToString().Trim();
            }
        }

        /// <summary>
        /// The subQueries processed by the bot's graphmaster that contain the templates that 
        /// are to be converted into the collection of Sentences
        /// </summary>
        public List<Utils.SubQuery> SubQueries = new List<Utils.SubQuery>();

        /// <summary>
        /// The individual sentences produced by the bot that form the complete response
        /// </summary>
        public List<string> OutputSentences = new List<string>();

        /// <summary>
        /// The individual sentences that constitute the raw input from the user
        /// </summary>
        public List<string> InputSentences = new List<string>();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="user">The user for whom this is a result</param>
        /// <param name="bot">The bot providing the result</param>
        /// <param name="request">The request that originated this result</param>
        public Result(BotUser user, AIMLBot bot, Request request)
        {
            this.user = user;
            this.bot = bot;
            this.request = request;
            this.request.result = this;
        }

        /// <summary>
        /// Returns the raw output from the bot
        /// </summary>
        /// <returns>The raw output from the bot</returns>
        public override string ToString()
        {
            return this.Output;
        }

        /// <summary>
        /// Checks that the provided sentence ends with a sentence splitter
        /// </summary>
        /// <param name="sentence">the sentence to check</param>
        /// <returns>True if ends with an appropriate sentence splitter</returns>
        private bool checkEndsAsSentence(string sentence)
        {
            foreach (string splitter in this.bot.Splitters)
            {
                if (sentence.Trim().EndsWith(splitter))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
