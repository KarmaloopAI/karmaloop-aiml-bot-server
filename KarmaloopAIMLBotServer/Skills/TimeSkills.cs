using System;

namespace KarmaloopAIMLBotServer.Skills
{
	public static class TimeSkills
	{
		public static SkillResult GetTime(SkillParams skillParams)
		{
			string time = DateTime.Now.ToString("hh:mm tt");

			return new SkillResult(time, time);
		}
	}
}

