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
        }

        public FfLogsService _ffLogsService { get; set; }
        public XivAppService _xivAppService { get; set; }

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

    }
}
