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

        public string PercentileClassification(int percentile)
        {
            if (percentile <= 24)
                return "Grey";

            if (percentile <= 49 && percentile >= 25)
                return "Green";

            if (percentile <= 74 && percentile >= 50)
                return "Blue";

            if (percentile <= 94 && percentile >= 75)
                return "Purple";

            if (percentile <= 99 && percentile >= 95)
                return "Orange";

            if (percentile == 100)
                return "Yellow";

            return "No ranking";
        }

        public string IconCode(string colorCode)
        {
            switch (colorCode)
            {
                case "Grey":
                    return "3_96x96";
                case "Green":
                    return "7_96x96";
                case "Blue":
                    return "6_96x96";
                case "Purple":
                    return "4_96x96";
                case "Orange":
                    return "1_96x96";
                case "Yellow":
                    return "5_96x96";
            }

            return "0_96x96";
        }
    }
}
