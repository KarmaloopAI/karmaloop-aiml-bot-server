using System;
using System.Text.RegularExpressions;
using System.Text;

namespace KarmaloopAIMLBot.Normalize
{
    /// <summary>
    /// Strips any illegal characters found within the input string. Illegal characters are referenced from
    /// the bot's Strippers regex that is defined in the setup XML file.
    /// </summary>
    public class StripIllegalCharacters : KarmaloopAIMLBot.Utils.TextTransformer
    {
        public StripIllegalCharacters(KarmaloopAIMLBot.AIMLBot bot, string inputString) : base(bot, inputString)
        { }

        public StripIllegalCharacters(KarmaloopAIMLBot.AIMLBot bot)
            : base(bot) 
        { }

        protected override string ProcessChange()
        {
            return this.bot.Strippers.Replace(this.inputString, " ");
        }
    }
}
