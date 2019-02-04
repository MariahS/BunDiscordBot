namespace BunBun.Discord.Model.FfLogs
{
    public class CharacterParse
    {
        public class Results
        {
            public int encounterID { get; set; }
            public string encounterName { get; set; }
            public string role { get; set; }
            public string spec { get; set; }
            public int rank { get; set; }
            public int outOf { get; set; }
            public int duration { get; set; }
            public object startTime { get; set; }
            public string reportID { get; set; }
            public int fightID { get; set; }
            public int difficulty { get; set; }
            public int characterID { get; set; }
            public string characterName { get; set; }
            public string server { get; set; }
            public int percentile { get; set; }
            public double ilvlKeyOrPatch { get; set; }
            public double total { get; set; }
            public bool estimated { get; set; }
        }
    }
}
