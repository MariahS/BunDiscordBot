using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord.Model.Xiv
{
    public class LodestoneCharacter
    {
        public class Pagination
        {
            public int Page { get; set; }
            public int PageNext { get; set; }
            public int PagePrevious { get; set; }
            public int PageTotal { get; set; }
            public int Results { get; set; }
            public int ResultsPerPage { get; set; }
            public int ResultsTotal { get; set; }
        }

        public class Result
        {
            public string Avatar { get; set; }
            public int FeastMatches { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }
            public object Rank { get; set; }
            public object RankIcon { get; set; }
            public string Server { get; set; }
        }

        public class RootObject
        {
            public Pagination Pagination { get; set; }
            public List<Result> Results { get; set; }
        }
    }
}
