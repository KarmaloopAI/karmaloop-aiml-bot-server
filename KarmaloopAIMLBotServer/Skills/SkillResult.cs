using System;
using System.Collections.Generic;

namespace KarmaloopAIMLBotServer.Skills
{
	/// <summary>
    /// Packages the result of an executed skill
    /// </summary>
	public class SkillResult
	{
        #region Properties

		/// <summary>
        /// Simple result string, to be appended to response utterance
        /// </summary>
        public string SimpleResult {
			get;
			set;
		}

		/// <summary>
        /// Complex result string with additional data
        /// </summary>
		public string RawResult {
			get;
			set;
		}

		/// <summary>
        /// Key value pairs as result which can then be used to replace value placeholders in a response utterance
        /// </summary>
		public Dictionary<string, string> KeyValues
		{
			get;
			set;
		}

		/// <summary>
        /// Type of the result, e.g. Simple, KeyValuePairs etc.
        /// </summary>
		public ResultType Type {
			get;
			set;
		}
        #endregion

        #region Constructor

		/// <summary>
        /// Simple result constructor
        /// </summary>
        /// <param name="simpleResult"></param>
        public SkillResult (string simpleResult)
		{
			this.SimpleResult = this.RawResult = simpleResult;
			this.Type = ResultType.Simple;
		}

		/// <summary>
        /// Simple result constructor with complex raw result string
        /// </summary>
        /// <param name="simpleResult">Simple result string</param>
        /// <param name="rawResult">Raw result string</param>
		public SkillResult(string simpleResult, string rawResult)
		{
			this.SimpleResult = simpleResult;
			this.RawResult = rawResult;
		}

		/// <summary>
        /// Key Value pair result constructor. Returns skills results as key value pairs.
        /// </summary>
        /// <param name="keyValues"></param>
		public SkillResult(Dictionary<string, string> keyValues)
        {
			this.KeyValues = keyValues;
			this.Type = ResultType.KeyValuePairs;
        }

        #endregion
    }

	/// <summary>
    /// Enumeration for result types
    /// </summary>
    public enum ResultType
    {
		Simple,
		KeyValuePairs
    }
}