using BunBun.Discord.Model.Xiv;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BunBun.Discord.Services
{
    public class XivAppService
    {
        private static readonly string key = Environment.GetEnvironmentVariable("xivKey");

        public MarketItem.ItemData GetItemMarketInfo(int itemId)
        {
            // limit list by 10
            string url = "https://xivapi.com/market/hyperion/items/" + itemId + "&limit=15" + "?" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<MarketItem.ItemData>(req.Content.ReadAsStringAsync().Result);

            return r;
        }

        public List<int> GetItemIdByName(string itemName)
        {
            var idList = new List<int>();
            var searchItem = new Search();
            if (itemName.Contains(" "))
            {
                itemName = itemName.Replace(" ", "+");
            }

            string url = "https://xivapi.com/search?indexes=item&filters=ItemSearchCategory.ID%3E%3D1&columns=ID%2CIcon%2CName%2CLevelItem%2CRarity%2CItemSearchCategory.Name%2CItemSearchCategory.ID%2CItemKind.Name&string=" + itemName + "&limit=5&" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<IndexedItemSearch>(req.Content.ReadAsStringAsync().Result);

            foreach (var item in r.Results)
            {
                idList.Add(item.ID);
            }

            return idList;

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
            catch (Exception)
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

        public List<LodestoneNews.RootObject> GetLodestoneNews()
        {
            string url = "https://xivapi.com/lodestone/news" + "?" + key;
            HttpResponseMessage req = url.GetAsync().Result;
            var r = JsonConvert.DeserializeObject<List<LodestoneNews.RootObject>>(req.Content.ReadAsStringAsync().Result);

            return r;
        }
    }
}
