using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public class Folders : CollectionBase
    {
        public void Add(string path)
        {
            List.Add(path);
        }

        public void Remove(string path)
        {
            List.Remove(path);
        }

        public string this[int index]
        {
            get { return (string)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Grabs the CSGO folder on startup, if the folders.json is empty or doesn't exist
        /// </summary>
        public static string GetCSGOReplayPath()
        {
            string steamPath = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamPath", null);

            if (steamPath != null)
            {
                steamPath += "/steamapps/common/Counter-Strike Global Offensive/csgo/replays";
                return steamPath;
            }
            else
                return null;
        }
    }
}
