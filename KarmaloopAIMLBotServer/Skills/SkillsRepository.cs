using System;
using System.Collections.Generic;

namespace KarmaloopAIMLBotServer.Skills
{
	public class SkillsRepository
	{
		#region Properties and Static accessor

		/// <summary>
		/// The m instance.
		/// </summary>
		private static SkillsRepository m_Instance = null;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static SkillsRepository Instance {
			get {
				if (m_Instance == null){
					m_Instance = new SkillsRepository ();
					m_Instance.InitSkillsIndex();
				}

				return m_Instance;
			}
		}

		/// <summary>
		/// Gets or sets the index of the skills.
		/// </summary>
		/// <value>The index of the skills.</value>
		public Dictionary<string,Func<SkillParams,SkillResult>> SkillsIndex {
			get;
			set;
		}

		/// <summary>
		/// The skill request token. This can be used to search the response from bot to identify if the bot wants a skill used
		/// to complete the answer.
		/// </summary>
		public const string SkillRequestToken = "{{skill:";
		#endregion

		#region Private Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="KarmaloopAIMLBotServer.Skills.SkillsRepository"/> class.
		/// </summary>
		private SkillsRepository ()
		{
		}

		#endregion

		#region Methods
        /// <summary>
        /// Init the Skills index
        /// </summary>
		protected virtual void InitSkillsIndex()
		{
			this.SkillsIndex = new Dictionary<string, Func<SkillParams, SkillResult>>();	
			this.SkillsIndex.Add("time", TimeSkills.GetTime);
			this.SkillsIndex.Add("weather", WeatherSkills.GetWeather);
			this.SkillsIndex.Add("scriptedskill", ScriptedSkill.RunScriptedSkill);
			
		}

        /// <summary>
        /// Execute the skill code to perform skill action.
        /// </summary>
        /// <param name="skillParams"></param>
        /// <returns>Result from the executed skill</returns>
		public SkillResult ExecuteSkill(SkillParams skillParams)
		{
			SkillResult result = null;
			if(this.SkillsIndex.ContainsKey(skillParams.SkillName))
			{
				result = this.SkillsIndex[skillParams.SkillName].Invoke(skillParams);
			}

			return (result != null ? result : new SkillResult(string.Empty));
		}

        /// <summary>
        /// Process any skills that the response from the engine might have invoked.
        /// </summary>
        /// <param name="responseStatement"></param>
        /// <returns></returns>
        public string ProcessSkills(string responseStatement)
        {
            if (responseStatement.Contains(SkillsRepository.SkillRequestToken))
            {
                int start = responseStatement.IndexOf("{{");
                int end = responseStatement.IndexOf("}}") - start + 2;
                string rawSkill = responseStatement.Substring(start, end);
                if (SkillParams.ValidateRawSkillString(rawSkill))
                {
                    SkillResult result = SkillsRepository.Instance.ExecuteSkill(new SkillParams(rawSkill));

					if (result.Type == ResultType.Simple)
						responseStatement = responseStatement.Replace(rawSkill, result.SimpleResult);
					else if (result.Type == ResultType.KeyValuePairs)
                    {
						// Response is in form of key value pairs.
						// 1. Remove skill invocation raw skill string
						responseStatement = responseStatement.Replace(rawSkill, string.Empty);

						// 2. Replace response key placeholders with values
						foreach (KeyValuePair<string, string> kv in result.KeyValues)
                        {
							responseStatement = responseStatement.Replace("{{key:" + kv.Key + "}}", kv.Value);
                        }
                    }
                }
            }

            return responseStatement;
        }
		#endregion
	}
}

