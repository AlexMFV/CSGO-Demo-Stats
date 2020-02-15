using DemoInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Demo_Stats
{
    public class Player
    {
        //General Variables
        public string name { get; set; }
        public int playerID { get; set; }
        public string steamID { get; set; }
        public int rank { get; set; } //Number of the rank
        public Team teamID { get; set; } //{Spectate, 1}, {T, 2}, {CT, 3}
        public BitmapImage avatar { get; set; }
        public string avatarUrl { get; set; }
        public int rankChange { get; set; } //0 = No Change, -1 = Derank, 1 = Rankup (maybe)
        public int winCount { get; set; }
        public Uri rankUri
        {
            get
            {
                if (rank >= 0 && rank <= 18)
                    return new Uri(("../Images/Ranks/skillgroup" + this.rank + ".png"), UriKind.Relative);
                else
                    return null;
            }
        }

        //Game Stats
        public int kills { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
        public double kdr { get; set; }
        public int hs { get; set; }
        public double hs_percent { get; set; }
        public int tk { get; set; } //Team kill or Friendly Fire
        public int entry_kills { get; set; }
        public int bombs_defused { get; set; }
        public int bombs_planted { get; set; }
        public double accuracy { get; set; }
        public int mvps { get; set; }
        public int score { get; set; }
        public int atd { get; set; } //Average Time of Death in seconds
        public int hltv_score { get; set; }
        public int esea_rws { get; set; }
        public int _1k { get; set; }
        public int _2k { get; set; }
        public int _3k { get; set; }
        public int _4k { get; set; }
        public int _5k { get; set; }
        public int trade_kills { get; set; }
        public int trade_death { get; set; }
        public int kpr { get; set; } //kills per round
        public int apr { get; set; } //assists per round
        public int dpr { get; set; } //death per round
        public int adr { get; set; } //average damage round
        public int tdd { get; set; } //Total damage dealt
        public int tda { get; set; } //Total damage armor
        public int _1v1 { get; set; }
        public int _1v2 { get; set; }
        public int _1v3 { get; set; }
        public int _1v4 { get; set; }
        public int _1v5 { get; set; }
        public bool vac_banned { get; set; }
        public bool ow_banned { get; set; } //Overwatch banned

        public Player(Team _team, string _name, long _steamID, int _playerID)
        {
            teamID = _team;
            name = _name;
            steamID = _steamID.ToString();
            playerID = _playerID;
        }
    }
}
