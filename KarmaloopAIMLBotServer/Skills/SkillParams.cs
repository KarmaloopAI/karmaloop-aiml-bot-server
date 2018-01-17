using System;
using System.Collections.Generic;

namespace KarmaloopAIMLBotServer.Skills
{
	public class SkillParams : Dictionary<string, string>
	{
		public string SkillName {
			get;
			set;
		}

		public SkillParams ()
			: base ()
		{
		}

		public SkillParams (string rawSkillString)
		{
			if (ValidateRawSkillString (rawSkillString)) {
				string cleanSkillString = rawSkillString.Replace ("{{", string.Empty).Replace ("}}", string.Empty);
				string[] paramsBreakup = cleanSkillString.Split (',');
				if (paramsBreakup.Length > 0) {
					foreach (string param in paramsBreakup) {
						string[] paramValBreakup = param.Split (':');
						if (paramValBreakup.Length > 1) {
							switch (paramValBreakup [0].ToLower ()) {
							case "skill":
								this.SkillName = paramValBreakup [1];
								break;
							default:
								this.Add(paramValBreakup [0].ToLower (), paramValBreakup [1]);
								break;
							}
						}
					}
				}
			}
		}

		public new SkillParams Add (string paramName, string value)
		{
			this[paramName.ToLower()] = value;

			return this;
		}

		public static bool ValidateRawSkillString (string rawSkillString)
		{
			return rawSkillString.StartsWith ("{{") && rawSkillString.EndsWith ("}}") && rawSkillString.Contains (":");
		}
	}
}

