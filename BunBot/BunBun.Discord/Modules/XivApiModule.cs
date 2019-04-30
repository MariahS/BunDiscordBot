using BunBun.Discord.Services;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
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
        private static List<string> ItemSearchList { get; set; }

        [Command("market")]
        public async Task PostMarketPrices([Remainder] string itemName)
        {
            // feature is currently down :c
            //var itemIdList = _apiService.GetItemIdByName(itemName);
            //var searchList = new List<string>();

            //string reply = "";
            //var replyString = new List<string>();
            //var eb = new EmbedBuilder();

            //for (int i = 0; i < itemIdList.Count; i++)
            //{
            //    var item = _apiService.GetItemById(itemIdList[i]);
            //    int count = i + 1;
            //    reply = count.ToString() + " " + item.Name;
            //    searchList.Add(item.Name);
            //    replyString.Add(reply);
            //}

            //ItemSearchList = searchList;

            //eb.AddField("Select the item yer looking fer:", string.Join("\n", replyString));
            //await Context.Channel.SendMessageAsync("", false, eb.Build());

            await Context.Channel.SendMessageAsync("My little bunnies at the market are on strike. Check back later for an update. :c");
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

        [Command("select")]
        public async Task SelectItemToSearch(int selection)
        {
            int min = 10;

            // check if item list is populated
            if (ItemSearchList == null)
            {
                await Context.Channel.SendMessageAsync("Please search fer yer item first by using the market command!");
            }
            else
            {
                var index = selection - 1;
                var itemName = ItemSearchList[index].Trim();
                var itemId = _apiService.GetItemIdByName(itemName);
                var itemInfo = _apiService.GetItemById(itemId[0]);

                // get market prices
                var marketPrices = _apiService.GetItemMarketInfo(itemId[0]);

                var eb = new EmbedBuilder();

                eb.Title = itemInfo.Name;
                eb.ThumbnailUrl = "https://xivapi.com/" + itemInfo.Icon.Replace("\\", "");
                eb.WithDescription(itemInfo.Description);

                if (marketPrices.prices.Count > 0)
                {
                    if (marketPrices.prices.Count < min)
                    {
                        min = marketPrices.prices.Count;
                    }
                    for (int i = 0; i < min; i++)
                    {
                        var replyString = new List<string>();

                        string quality = "NQ";
                        if (marketPrices.prices[i].IsHQ)
                            quality = "HQ";

                        replyString.Add(marketPrices.prices[i].Quantity + " " + quality + " units for " + marketPrices.prices[i].PricePerUnit + " each");
                        replyString.Add("Total of " + marketPrices.prices[i].PriceTotal + " gil");
                        eb.AddField("---", string.Join("\n", replyString));
                    }

                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else
                {
                    // add price history response
                    await Context.Channel.SendMessageAsync("Ain't none on the market :c");
                } 
            }

            
        }

    }
}
