using System;
using System.Collections.Generic;
using System.Text;

namespace KarmaloopAIMLBot.Normalize
{
    /// <summary>
    /// Normalizes the input text into upper case
    /// </summary>
    public class MakeCaseInsensitive : KarmaloopAIMLBot.Utils.TextTransformer
    {
        public MakeCaseInsensitive(KarmaloopAIMLBot.AIMLBot bot, string inputString) : base(bot, inputString)
        { }

        public MakeCaseInsensitive(KarmaloopAIMLBot.AIMLBot bot) : base(bot) 
        { }

        protected override string ProcessChange()
        {
            return this.inputString.ToUpper();
        }

        /// <summary>
        /// An ease-of-use static method that re-produces the instance transformation methods
        /// </summary>
        /// <param name="input">The string to transform</param>
        /// <returns>The resulting string</returns>
        public static string TransformInput(string input)
        {
            return input.ToUpper();
        }
    }
}
