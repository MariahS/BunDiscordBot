using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BunBun.Discord.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        public InfoModule()
        {
            _apiService = new XivAppService();
        }

        public XivAppService _apiService {get;set;}

        [Command("info")]
        public async Task Info()
        {
            string reply = "Halooo, I Bun Bot. I still got buggies. Bare with me until I'm perfect. :rabbit:";
            await ReplyAsync(reply);
        }

        [Command("\U0001f955")]
        public async Task FeedBunnyCarrot()
        {
            string url = "https://tenor.com/SKqG.gif";

            await Context.Channel.SendMessageAsync(url);
        }

        [Command("\U0001F4A9")]
        public async Task FeedBunnyPoop()
        {
            string url = "https://tenor.com/N9JZ.gif";

            await Context.Channel.SendMessageAsync(url);
        }

        [Command("help")]
        public async Task CommandInfo()
        {
            var eb = new EmbedBuilder();
            eb.Title = "Commands";
            eb.Description = "I don't have many commands yet but if you feed me a carrot, I would be happy. c: Just @ me with the command and I can help ya!";
            eb.AddField("\t\u2022 help", "I tell you the helpful stuff");
            eb.AddField("\t\u2022 market [item]", "Replace [item] with whatever item you wanna search on the market board. I tell you the prices.");
            eb.AddField("\t\u2022 parse [server] [character]", "Replace [server] and [character] in your search and I can tell you if they good or not. :wink:");

            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
    }
}
