using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord.Model
{
    public class Search
    {
        public SearchData.Pagination Pagination { get; set; }
        public List<SearchData.Result> Results { get; set; }
    }

    public class SearchData
    {
        public class Pagination
        {
            public int? Page { get; set; }
            public int? PageNext { get; set; }
            public int? PagePrev { get; set; }
            public int? PageTotal { get; set; }
            public int? Results { get; set; }
            public int? ResultsPerPage { get; set; }
            public int? ResultsTotal { get; set; }
        }

        public class Result
        {
            public int ID { get; set; }
            public string Icon { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}
