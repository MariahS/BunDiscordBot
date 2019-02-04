using BunBun.Discord.Model.Xiv;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace BunBun.Discord.Services
{
    public class XivAppService
    {
        private static readonly string key = Environment.GetEnvironmentVariable("xivKey");

        public MarketItem.ItemData GetItemMarketInfo(int itemId)
        {
            // limit list by 10
            string url = "https://xivapi.com/market/hyperion/items/" + itemId + "&limit=10" + "?" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<MarketItem.ItemData>(req.Content.ReadAsStringAsync().Result);

            return r;
        }

        public int GetItemIdByName(string itemName)
        {
            // Make this more precise
            // Maybe give a list of related items
            // And have user select the correct search
            var searchItem = new Search();
            if (itemName.Contains(" "))
            {
                itemName = itemName.Replace(" ", "+");
            }

            string url = "https://xivapi.com/search?string=" + itemName + "&" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<Search>(req.Content.ReadAsStringAsync().Result);
            searchItem = r;

            foreach (var item in searchItem.Results)
            {
                if (item.Url.Contains("Item"))
                {
                    return item.ID;
                }
            }

            return 0;
        }

        public Item.RootObject GetItemById(int itemId)
        {
            var r = new Item.RootObject();
            try
            {
                string url = "https://xivapi.com/item/" + itemId + "?" + key;
                HttpResponseMessage req = url.GetAsync().Result;
                r = JsonConvert.DeserializeObject<Item.RootObject>(req.Content.ReadAsStringAsync().Result);
            }
            catch (System.Exception)
            {

                return r;
            }
            

            return r;
        }

        public LodestoneCharacter.RootObject GetCharacter(string name, string server)
        {
            name = name.Replace(" ","+");
            string url = "https://xivapi.com/character/search?name=" + name + "&server=" + server + "&" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<LodestoneCharacter.RootObject>(req.Content.ReadAsStringAsync().Result);

            return r;
        }
    }
}
