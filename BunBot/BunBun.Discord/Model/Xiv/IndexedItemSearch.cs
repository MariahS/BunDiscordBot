using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord.Model.Xiv
{
        public class IndexedItemSearch
        {
            public Pagination Pagination { get; set; }
            public List<Result> Results { get; set; }
            public int SpeedMs { get; set; }
        }

        public class Pagination
        {
            public int? Page { get; set; }
            public int? PageNext { get; set; }
            public object PagePrev { get; set; }
            public int? PageTotal { get; set; }
            public int? Results { get; set; }
            public int? ResultsPerPage { get; set; }
            public int? ResultsTotal { get; set; }
        }

        public class ItemKind
        {
            public string Name { get; set; }
        }

        public class ItemSearchCategory
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class Result
        {
            public int ID { get; set; }
            public string Icon { get; set; }
            public ItemKind ItemKind { get; set; }
            public ItemSearchCategory ItemSearchCategory { get; set; }
            public int LevelItem { get; set; }
            public string Name { get; set; }
            public int Rarity { get; set; }
        }
    
}
