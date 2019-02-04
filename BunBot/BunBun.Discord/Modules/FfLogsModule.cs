using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
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
            // Get character information
            var character = _xivAppService.GetCharacter(name, server);
            
            var eb = new EmbedBuilder();

            eb.Title = characterParse[0].characterName;
            eb.ThumbnailUrl = character.Results[0].Avatar;

            // set a minimum of results
            // specify class
            foreach (var parse in characterParse)
            {
                reply = parse.total + " as " + parse.spec;
                eb.AddField(parse.encounterName, reply);
            }
            
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }

    }
}
