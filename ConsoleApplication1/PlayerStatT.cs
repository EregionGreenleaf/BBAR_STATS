using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAR_Stat
{
    class PlayerStatT
    {
        public long? Rank { get; set; }
        public string Name { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public double? WLr { get; set; }
        public int? Kills { get; set; }
        public int? Deaths { get; set; }
        public double? KDr { get; set; }
        public long? GamesPlayed { get; set; }
        public int? AvarageMatchScore { get; set; }
        public int? WebPage { get; set; }

        public PlayerStatT(long? rank = null, string name = null, int? wins = null,
                           int? losses = null, double? wlr = null, int? kills = null, 
                           int? deaths = null, double? kdr = null, long? gamesPlayed = null,
                           int? avarageMatchScore = null, int? webPage = null)
        {
            Rank = rank;
            Name = name;
            Wins = wins;
            Losses = losses;
            WLr = wlr;
            Kills = kills;
            Deaths = deaths;
            KDr = kdr;
            GamesPlayed = gamesPlayed;
            AvarageMatchScore = avarageMatchScore;
            WebPage = webPage;
        }
    }
}
