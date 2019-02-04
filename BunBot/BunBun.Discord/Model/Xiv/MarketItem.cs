using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord.Model.Xiv
{
    public class MarketItem
    {
        public class RootObject
        {
            public IList<ItemData> data { get; set; }
        }

        public class ItemData
        {
            public MarketItem item { get; set; }
            public Lodestone lodestone { get; set; }
            public Town town { get; set; }
            public List<Prices> prices { get; set; }

            public class MarketItem
            {
                public int ID { get; set; }
                public string Icon { get; set; }
                public string Name { get; set; }
                public string Name_de { get; set; }
                public string Name_en { get; set; }
                public string Name_fr { get; set; }
                public string Name_ja { get; set; }
                public int Rarity { get; set; }
            }

            public class Lodestone
            {
                public string Icon { get; set; }
                public string IconHq { get; set; }
                public string LodestoneId { get; set; }
            }

            public class Town
            {
                public int ID { get; set; }
                public string Icon { get; set; }
                public string Name { get; set; }
                public string Name_de { get; set; }
                public string Name_en { get; set; }
                public string Name_fr { get; set; }
                public string Name_ja { get; set; }
                public string Url { get; set; }
            }

            public class Prices
            {
                public string CraftSignature { get; set; }
                public int ID { get; set; }
                public bool IsCrafted { get; set; }
                public bool IsHQ { get; set; }
                public List<object> Materia { get; set; }
                public int PricePerUnit { get; set; }
                public int PriceTotal { get; set; }
                public int Quantity { get; set; }
                public string RetainerName { get; set; }
                public int Stain { get; set; }
                public Town Town { get; set; }
            }

        }
    }
}
