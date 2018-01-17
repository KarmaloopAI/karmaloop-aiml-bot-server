using System;
using System.Text;
using System.Text.RegularExpressions;

namespace KarmaloopAIMLBot.Normalize
{
    /// <summary>
    /// Checks the text for any matches in the bot's substitutions dictionary and makes
    /// any appropriate changes.
    /// </summary>
    public class ApplySubstitutions : KarmaloopAIMLBot.Utils.TextTransformer
    {
        public ApplySubstitutions(KarmaloopAIMLBot.AIMLBot bot, string inputString)
            : base(bot, inputString)
        { }

        public ApplySubstitutions(KarmaloopAIMLBot.AIMLBot bot)
            : base(bot)
        { }

        /// <summary>
        /// Produces a random "marker" string that tags text that is already the result of a substitution
        /// </summary>
        /// <param name="len">The length of the marker</param>
        /// <returns>the resulting marker</returns>
        private static string getMarker(int len)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            StringBuilder result = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < len; i++)
            {
                result.Append(chars[r.Next(chars.Length)]);
            }
            return result.ToString();
        }

        protected override string ProcessChange()
        {
            return ApplySubstitutions.Substitute(this.bot, this.bot.Substitutions, this.inputString);
        }

        /// <summary>
        /// Static helper that applies replacements from the passed dictionary object to the 
        /// target string
        /// </summary>
        /// <param name="bot">The bot for whom this is being processed</param>
        /// <param name="dictionary">The dictionary containing the substitutions</param>
        /// <param name="target">the target string to which the substitutions are to be applied</param>
        /// <returns>The processed string</returns>
        public static string Substitute(KarmaloopAIMLBot.AIMLBot bot, KarmaloopAIMLBot.Utils.SettingsDictionary dictionary, string target)
        {
            string marker = ApplySubstitutions.getMarker(5);
            string result = target;
            foreach (string pattern in dictionary.SettingNames)
            {
                string p2 = ApplySubstitutions.makeRegexSafe(pattern);
                //string match = "\\b"+@p2.Trim().Replace(" ","\\s*")+"\\b";
                string match = "\\b" + p2.TrimEnd().TrimStart() + "\\b";
                string replacement = marker+dictionary.GetSetting(pattern).Trim()+marker;
                result = Regex.Replace(result, match, replacement, RegexOptions.IgnoreCase);
            }

            return result.Replace(marker, "");
        }


        /// <summary>
        /// Given an input, escapes certain characters so they can be used as part of a regex
        /// </summary>
        /// <param name="input">The raw input</param>
        /// <returns>the safe version</returns>
        private static string makeRegexSafe(string input)
        {
            string result = input.Replace("\\","");
            result = result.Replace(")", "\\)");
            result = result.Replace("(", "\\(");
            result = result.Replace(".", "\\.");
            return result;
        }
    }
}
