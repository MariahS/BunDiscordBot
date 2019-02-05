using BunBun.Discord.Model.FfLogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BunBun.Discord
{
    public class HelperExtentions
    {
        public class CharacterParseComparer : IEqualityComparer<CharacterParse.Results>
        {
            public bool Equals(CharacterParse.Results x, CharacterParse.Results y)
            {
                return x.encounterID == y.encounterID;
            }

            public int GetHashCode(CharacterParse.Results x)
            {
                return x.encounterID.GetHashCode();
            }
        }
    }
}
