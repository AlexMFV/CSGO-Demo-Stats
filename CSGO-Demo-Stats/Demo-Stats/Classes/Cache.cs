using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo_Stats
{
    class Cache
    {
        //Get the %appdata% dir
        static string appdata_dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        #region Loads

        public static void LoadAll()
        {

        }

        public static Accounts LoadAccounts()
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv");
            Accounts collection = new Accounts();

            //NEEDS FIX: If the Path exists but the file doesn't it enters the If but then cant read file
            if (Directory.Exists(cacheDir))
            {
                JArray arr = JArray.Parse(File.ReadAllText(cacheDir + "\\steam_accounts.json"));
                foreach(JObject obj in arr)
                {
                    Account acc = Parser.ParseAccount(obj.Property("SteamID").Value.ToString());
                    collection.Add(acc);
                }
            }

            return collection;
        }

        #endregion

        #region Saves

        public static void SaveAll()
        {

        }

        public static void SaveAccounts(Accounts collection)
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv");

            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);

            //From Accounts Collection To JSON To File
            using (StreamWriter file = File.CreateText(cacheDir + "\\steam_accounts.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                JArray arr = new JArray();

                foreach(Account acc in collection)
                {
                    JObject toAdd = new JObject();
                    toAdd.Add("SteamID", acc.steamID);
                    toAdd.Add("Name", acc.personaName);
                    arr.Add(toAdd);
                }

                arr.WriteTo(writer);
            }
        }

        #endregion
    }
}
