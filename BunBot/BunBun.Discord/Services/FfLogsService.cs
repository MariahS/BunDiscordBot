using BunBun.Discord.Model.FfLogs;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace BunBun.Discord.Services
{
    public class FfLogsService
    {
        private static readonly string key = Environment.GetEnvironmentVariable("fflogsKey");

        public List<CharacterParse.Results> GetParseRanking(string name, string server)
        {
            name = name.Replace(" ", "%20");
            string url = "https://www.fflogs.com:443/v1/rankings/character/" + name + "/" + server + "/NA?api_key=" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<List<CharacterParse.Results>>(req.Content.ReadAsStringAsync().Result);

            return r;
        }

        public List<IGrouping<CharacterParse.Results, CharacterParse.Results>> GroupParses(List<CharacterParse.Results> parses)
        {
            
            var comparer = new HelperExtentions.CharacterParseComparer();
            var duplicates = parses.GroupBy(x => x, comparer)
                .ToList();

            return duplicates;
        }
    }
}
