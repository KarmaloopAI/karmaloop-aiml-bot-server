using System;

namespace KarmaloopAIMLBotServer.Skills
{
	public class SkillResult
	{
		public string SimpleResult {
			get;
			set;
		}

		public string RawResult {
			get;
			set;
		}

		public SkillResult (string simpleResult)
		{
			this.SimpleResult = this.RawResult = simpleResult;
		}

		public SkillResult (string simpleResult, string rawResult)
		{
			this.SimpleResult = simpleResult;
			this.RawResult = rawResult;
		}
	}
}

