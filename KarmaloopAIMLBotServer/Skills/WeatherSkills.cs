using System;
using System.Dynamic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace KarmaloopAIMLBotServer.Skills
{
    public static class WeatherSkills
    {
        public static SkillResult GetWeather(SkillParams skillParams)
        {
            string location = skillParams["location"];
            string engString = string.Empty;

            string url = "https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='{{location}}')&format=json";
            url = url.Replace("{{location}}", location);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            HttpWebResponse webResp = (HttpWebResponse)req.GetResponse();
            StreamReader resp = new StreamReader(webResp.GetResponseStream());
            string rawJson = resp.ReadToEnd();

            dynamic js = JsonConvert.DeserializeObject<dynamic>(rawJson);
            if (js.query.results != null)
            {
                string temp = js.query.results.channel.item.condition.temp;
                string unit = js.query.results.channel.units.temperature;
                string condition = js.query.results.channel.item.condition.text;
                engString = temp + " degrees " + (unit == "F" ? "Fahrenheit" : "Celsius") + " and the condition is " + condition;

            }
            else
            {
                engString = " unknown.";
            }

            return new SkillResult(engString);
        }


    }
}

