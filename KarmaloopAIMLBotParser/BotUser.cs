using KarmaloopAIMLBot.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace KarmaloopAIMLBot
{
    /// <summary>
    /// Identifies the user for the bot. Holds conversation info and related properties to help restore conversation.
    /// </summary>
    public class BotUser
    {
        #region Attributes

        /// <summary>
        /// A unique identifier for the user. Supposed to be a GUID.
        /// </summary>
        private Guid m_UserID;

        /// <summary>
        /// The GUID that identifies this user to the bot
        /// </summary>
        public Guid UserID
        {
            get { return this.m_UserID; }
            set { this.m_UserID = value; }
        }

        /// <summary>
        /// A handle of the bot this user is using
        /// </summary>
        public AIMLBot Bot { get; set; }

        /// <summary>
        /// Historic collection of results
        /// </summary>
        private List<Result> Results = new List<Result>();

        /// <summary>
        /// Gets the Topic.
        /// </summary>
        public string Topic
        {
            get
            {
                return this.Predicates.GetSetting("topic");
            }
        }

        /// <summary>
        /// Predicates for this user.
        /// </summary>
        public SettingsDictionary Predicates;

        /// <summary>
        /// Last response by the bot
        /// </summary>
        public Result LastResult
        {
            get
            {
                Result res;
                if (this.Results.Count > 0)
                {
                    res = this.Results[0] as Result;
                }
                else
                {
                    res = null;
                }

                return res;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// BotUser Constructor
        /// </summary>
        /// <param name="userID">The ID of the user</param>
        /// <param name="bot">the bot the user is connected to</param>
        public BotUser(Guid userID, AIMLBot bot)
        {
            this.m_UserID = userID;
            this.Bot = bot;
            this.Predicates = new SettingsDictionary(this.Bot);
            this.Bot.DefaultPredicates.Clone(this.Predicates);
            this.Predicates.AddSetting("topic", "*");
        }

        /// <summary>
        /// Returns the last result if available or *. Used for the "that" part of next path.
        /// </summary>
        /// <returns>Sentence that will be used for "that"</returns>
        public string GetLastBotResult()
        {
            if (this.LastResult != null)
            {
                return this.LastResult.RawOutput;
            }
            else
            {
                return "*";
            }
        }

        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetThat()
        {
            return this.GetThat(0, 0);
        }

        /// <summary>
        /// Returns the first sentence of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence of the output "n" steps ago from the bot</returns>
        public string GetThat(int n)
        {
            return this.GetThat(n, 0);
        }

        /// <summary>
        /// Returns the sentence numbered by "sentence" of the output "n" steps ago from the bot
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentenceIndex">the sentence number to get</param>
        /// <returns>the sentence numbered by "sentence" of the output "n" steps ago from the bot</returns>
        public string GetThat(int n, int sentenceIndex)
        {
            string that = string.Empty;

            if ((n >= 0) & (n < this.Results.Count))
            {
                Result historicResult = (Result)this.Results[n];
                if ((sentenceIndex >= 0) & (sentenceIndex < historicResult.OutputSentences.Count))
                {
                    that = historicResult.OutputSentences[sentenceIndex] as string;
                }
            }

            return that;
        }

        /// <summary>
        /// Returns the first sentence of the last output from the bot
        /// </summary>
        /// <returns>the first sentence of the last output from the bot</returns>
        public string GetResultSentence()
        {
            return this.GetResultSentence(0, 0);
        }

        /// <summary>
        /// Returns the first sentence from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <returns>the first sentence from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n)
        {
            return this.GetResultSentence(n, 0);
        }

        /// <summary>
        /// Returns the identified sentence number from the output from the bot "n" steps ago
        /// </summary>
        /// <param name="n">the number of steps back to go</param>
        /// <param name="sentenceIndex">the sentence number to return</param>
        /// <returns>the identified sentence number from the output from the bot "n" steps ago</returns>
        public string GetResultSentence(int n, int sentenceIndex)
        {
            string sentence = string.Empty;

            if ((n >= 0) & (n < this.Results.Count))
            {
                Result historicResult = this.Results[n] as Result;
                if ((sentenceIndex >= 0) & (sentenceIndex < historicResult.InputSentences.Count))
                {
                    sentence = historicResult.InputSentences[sentenceIndex] as string;
                }
            }
            return sentence;
        }

        /// <summary>
        /// Adds the latest result from the bot to the Results collection
        /// </summary>
        /// <param name="latestResult">the latest result from the bot</param>
        public void AddResult(Result latestResult)
        {
            this.Results.Insert(0, latestResult);
        }
        #endregion
    }
}