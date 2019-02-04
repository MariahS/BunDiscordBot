using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace BunBun.Discord.Modules
{
    public class XivApiModule : ModuleBase<SocketCommandContext>
    {
        public XivApiModule()
        {
            _apiService = new XivAppService();
        }

        public XivAppService _apiService { get; set; }

        [Command("market")]
        public async Task GetMarketPrices([Remainder] string itemName)
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


    }
}
