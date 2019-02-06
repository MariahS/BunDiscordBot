using BunBun.Discord.Model.FfLogs;
using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBun.Discord.Modules
{
    public class FfLogsModule : ModuleBase<SocketCommandContext>
    {
        public FfLogsModule()
        {
            _ffLogsService = new FfLogsService();
            _xivAppService = new XivAppService();
            _extentionHelper = new HelperExtentions();
        }

        public FfLogsService _ffLogsService { get; set; }
        public XivAppService _xivAppService { get; set; }
        public HelperExtentions _extentionHelper { get; set; }

        [Command("parse")]
        public async Task GetParse(string server, [Remainder] string name)
        {
            string reply = "";
            
            // Get parsing information
            var characterParse = _ffLogsService.GetParseRanking(name, server);
            if (characterParse.Count > 0)
            {
                // Get character information
                var character = _xivAppService.GetCharacter(name, server);
                var parseGroup = _ffLogsService.GroupParses(characterParse);


                var eb = new EmbedBuilder();

                eb.Title = characterParse[0].characterName;
                eb.ThumbnailUrl = character.Results[0].Avatar;

               
                foreach (var group in parseGroup)
                {
                    var replyString = new List<string>();
                    var section = group.Select(x => new { x.encounterName, x.spec, x.total }).ToList();

                    foreach (var record in section)
                    {
                        reply = "\t\u2022 " + record.total + " as " + record.spec;
                        replyString.Add(reply);
                    }

                    eb.AddField(section[0].encounterName, string.Join("\n", replyString));
                }

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync("Cannot find user on fflogs. :c");
            }

            
        }

        [Command("BestParse")]
        public async Task GetBestParse(string server, [Remainder] string name)
        {
            string reply = "";
            var replyString = new List<string>();
            // Get parsing information
            var characterParse = _ffLogsService.GetParseRanking(name, server);
            if (characterParse.Count > 0)
            {
                // Get character information
                var character = _xivAppService.GetCharacter(name, server);
                var bestParse = _ffLogsService.GetBestParseResults(characterParse);
                var percentile = _extentionHelper.PercentileClassification(bestParse.percentile);
                var iconCode = _extentionHelper.IconCode(percentile);
                string thumbnail = character.Results[0].Avatar.Replace("0_96x96", iconCode);

                
                var eb = new EmbedBuilder();
                eb.Title = characterParse[0].characterName;
                eb.ThumbnailUrl = thumbnail; //thumbnail edit here depending on stats of parse
                reply = "\t\u2022 " + bestParse.total + " as " + bestParse.spec;
                replyString.Add(reply);
                replyString.Add("\t\u2022 Percentile: " + bestParse.percentile + " " + percentile);
                eb.AddField(bestParse.encounterName,  string.Join("\n", replyString));              

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync("Cannot find user on fflogs. :c");
            }

        }
    }
}
