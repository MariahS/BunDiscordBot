using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord.Model.Xiv
{
    public class LodestoneNews
    {
        public class RootObject
        {
            public string Banner { get; set; }
            public string Html { get; set; }
            public int Time { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
        }
    }
}
