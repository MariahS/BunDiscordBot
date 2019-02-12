using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BunBun.Discord.Modules
{
    public class XivApiModule : ModuleBase<SocketCommandContext>
    {
        public XivApiModule()
        {
            _apiService = new XivAppService();
            LodestoneUrl = @"https://na.finalfantasyxiv.com";
        }

        public XivAppService _apiService { get; set; }
        private string LodestoneUrl { get; set; }

        [Command("market")]
        public async Task PostMarketPrices([Remainder] string itemName)
        {
            var itemId = _apiService.GetItemIdByName(itemName);
            var item = _apiService.GetItemById(itemId);

            string reply = "";
            var eb = new EmbedBuilder();
            int min = 5;

            if (item.IsUntradable == 0)
            {
                var result = _apiService.GetItemMarketInfo(itemId);
                eb.Title = item.Name;
                eb.ThumbnailUrl = "https://xivapi.com/" + item.Icon.Replace("\\", "");
                eb.WithDescription(item.Description);
                if (result.prices.Count < min)
                {
                    min = result.prices.Count;
                }

                for (int i = 0; i < min; i++)
                {
                    reply = result.prices[i].Quantity + " units for a total of " + result.prices[i].PriceTotal + " gil by " + result.prices[i].RetainerName;
                    eb.AddField("---", reply);
                }

                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                reply = item.Name + "is not available on the market.";
                await ReplyAsync(reply);
            }
        }

        // Eventually make this a timed task
        [Command("news")]
        public async Task PostLodestoneNews()
        {
            var news = _apiService.GetLodestoneNews();
            var currentDate = DateTime.Now.ToShortDateString();

            foreach (var article in news)
            {
                var dt = ConvertUnixToDateTime(article.Time);
                if (dt.ToShortDateString() == currentDate)
                {
                    var eb = new EmbedBuilder();
                    eb.Title = article.Title;
                    eb.Url = LodestoneUrl + article.Url;
                    eb.ImageUrl = article.Banner;

                    await ReplyAsync("", false, eb.Build());
                }
            }

            
        }

        public DateTime ConvertUnixToDateTime(double unixTime)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTime).ToLocalTime();
            return dt;
        }

    }
}
