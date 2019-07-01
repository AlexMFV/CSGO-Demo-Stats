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

        /// <summary>
        /// Loads the accounts from the JSON file, but doesn't request full info from API, just returns SteamID
        /// and account nickname.
        /// </summary>
        /// <returns>Collection of Accounts</returns>
        public static Accounts LoadAccountsBasic()
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv");
            Accounts collection = new Accounts();

            if (File.Exists(cacheDir + "\\steam_accounts.json"))
            {
                if (new FileInfo(cacheDir + "\\steam_accounts.json").Length > 0)
                {
                    JArray arr = JArray.Parse(File.ReadAllText(cacheDir + "\\steam_accounts.json"));
                    foreach (JObject obj in arr)
                    {
                        Account acc = new Account();
                        acc.steamID = obj.Property("SteamID").Value.ToString();
                        acc.personaName = obj.Property("Name").Value.ToString();
                        collection.Add(acc);
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Loads the full account details (request to the API), for the accounts stored in Cache
        /// </summary>
        /// <returns>Collection of Accounts</returns>
        public static Accounts LoadAccounts()
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv");
            Accounts collection = new Accounts();

            if (File.Exists(cacheDir + "\\steam_accounts.json"))
            {
                if (new FileInfo(cacheDir + "\\steam_accounts.json").Length > 0)
                {
                    JArray arr = JArray.Parse(File.ReadAllText(cacheDir + "\\steam_accounts.json"));
                    foreach (JObject obj in arr)
                    {
                        Account acc = Parser.ParseAccount(obj.Property("SteamID").Value.ToString());
                        collection.Add(acc);
                    }
                }
            }

            return collection;
        }

        public static AppSettings LoadSettings()
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv", "settings");
            AppSettings collection = new AppSettings();

            if (File.Exists(cacheDir + "\\user_data.json"))
                if (new FileInfo(cacheDir + "\\user_data.json").Length > 0)
                    collection = Parser.ParseAppSettings(JObject.Parse(File.ReadAllText(cacheDir + "\\user_data.json")));

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

        public static void SaveAppSettings(AppSettings settings)
        {
            string cacheDir = Path.Combine(appdata_dir, "alexmfv", "settings");

            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);

            //From Accounts Collection To JSON To File
            using (StreamWriter file = File.CreateText(cacheDir + "\\user_data.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                JObject obj = new JObject();
                obj.Add("SteamID", settings.selectedSteamID);
                //toAdd.Add("<variable_name>", <value>); //Add All the necessary settings values

                obj.WriteTo(writer);
            }
        }

        #endregion
    }
}
