using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public class AppDataMethods
    {
        //Returns the index (stored in cache) of the account to process
        public static int GetIndexFromSteamID(string id, Accounts collection)
        {
            for (int i = 0; i < collection.Count; i++)
                if (collection[i].steamID == id)
                    return i;
            return -1;
        }

        public static int GetIndexFromSteamPath(string path, Folders collection)
        {
            for (int i = 0; i < collection.Count; i++)
                if (collection[i] == path)
                    return i;
            return -1;
        }
    }
}
