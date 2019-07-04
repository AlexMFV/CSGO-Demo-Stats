using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public class AppSettings
    {
        //The SteamID of the user that was last selected
        public string selectedSteamID { get; set; }

        //The index of the path that is currently selected
        public string selectedSteamPath { get; set; }
    }
}
