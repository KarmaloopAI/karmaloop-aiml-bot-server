using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;

namespace KarmaloopAIMLBotServer.Skills
{
    /// <summary>
    /// Implementation of Scripted Skills. These skills are third party scripts that can be invoked on runtime.
    /// </summary>
    public static class ScriptedSkill
    {
        /// <summary>
        /// Entry point for executing the method that invokes the third party script
        /// </summary>
        /// <param name="skillParams"></param>
        /// <returns></returns>
        public static SkillResult RunScriptedSkill(SkillParams skillParams)
        {
            var scriptsPath = ConfigurationManager.AppSettings["scriptsPath"];
            if (ValidateParams(skillParams) && scriptsPath != null)
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = skillParams["interpreter"],
                    Arguments = string.Concat(scriptsPath, skillParams["scriptname"], " ", skillParams["args"]),
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var process = Process.Start(processStartInfo);
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                ScriptedSkillResponse js = JsonConvert.DeserializeObject<ScriptedSkillResponse>(output);

                // Build dictionary from key values in response
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (KeyValue kv in js.keyvalues)
                {
                    dict[kv.key] = kv.value;
                }

                SkillResult result = new SkillResult(dict);

                return result;
            }

            return new SkillResult("");
        }

        /// <summary>
        /// Validate parameters if they qualify for scripted skills invocation.
        /// </summary>
        /// <param name="skillParams"></param>
        /// <returns></returns>
        public static bool ValidateParams(SkillParams skillParams)
        {
            bool retVal = false;
            if (skillParams.ContainsKey("interpreter") && skillParams.ContainsKey("scriptname"))
            {
                retVal = true;
            }

            return retVal;
        }
    }

    /// <summary>
    /// Response model for the return JSON from a scripted skill
    /// </summary>
    public class ScriptedSkillResponse
    {
        public string message { get; set; }

        public List<KeyValue> keyvalues { get; set; }
    }

    /// <summary>
    /// KeyValue model for the key value pairs in response JSON
    /// </summary>
    public class KeyValue
    {
        public string key { get; set; }

        public string value { get; set; }
    }
}
