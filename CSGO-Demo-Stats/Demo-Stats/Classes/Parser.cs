using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Demo_Stats.Properties;
using Newtonsoft.Json.Linq;

namespace Demo_Stats
{
    public static class Parser
    {
        static string GrabJSONString(string id)
        {
            try
            {
                string _json;
                using (WebClient web = new WebClient())
                {
                    _json = web.DownloadString(Resources.API_ProfileURL1 + APIKey.GetKeyFromFile() + Resources.API_ProfileURL2 + id);
                }
                return _json;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static Account ParseAccount(string id)
        {
            try
            {
                Account newAcc = new Account();
                JObject objs = JObject.Parse(TrimAccount(GrabJSONString(id)));
                foreach (KeyValuePair<string, JToken> pair in objs)
                {
                    switch (pair.Key)
                    {
                        case "steamid": newAcc.steamID = (string)pair.Value; break;
                        case "communityvisibilitystate": newAcc.visibilityState = (string)pair.Value; break;
                        case "profilestate": newAcc.profileState = (string)pair.Value; break;
                        case "personaname": newAcc.personaName = (string)pair.Value; break;
                        case "lastlogoff": newAcc.lastlogoff = (string)pair.Value; break;
                        case "commentpermission": newAcc.commentPermission = (string)pair.Value; break;
                        case "profileurl": newAcc.profileURL = (string)pair.Value; break;
                        case "avatar": newAcc.avatar = (string)pair.Value; break;
                        case "personastate": newAcc.personaState = (string)pair.Value; break;
                        case "primaryclanid": newAcc.primaryClanID = (string)pair.Value; break;
                        case "timecreated": newAcc.timeCreated = (string)pair.Value; break;
                        case "personastateflags": newAcc.personaStateFlags = (string)pair.Value; break; 
                        case "loccountrycode": newAcc.locCountryCode = (string)pair.Value; break;
                        case "locstatecode": newAcc.locStateCode = (string)pair.Value; break;
                        case "loccityid": newAcc.locCityID = (string)pair.Value; break;
                    }
                }
                return newAcc;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static Account ParseAccountBasic(string id)
        {
            try
            {
                Account newAcc = new Account();
                string json = GrabJSONString(id);
                JObject objs = new JObject();

                //If it contains the empty array, then the SteamID is invalid
                if (!json.Contains("\"players\":[]"))
                {
                    objs = JObject.Parse(TrimAccount(json));

                    foreach (KeyValuePair<string, JToken> pair in objs)
                    {
                        switch (pair.Key)
                        {
                            case "steamid": newAcc.steamID = (string)pair.Value; break;
                            case "personaname": newAcc.personaName = (string)pair.Value; break;
                        }

                        if (newAcc.steamID != null && newAcc.personaName != null)
                            return newAcc;
                    }
                }
                else
                    throw new InvalidSteamID();

                return newAcc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AccountExists(string id, Accounts collection)
        {
            foreach (Account acc in collection)
                if (id.Contains(acc.steamID))
                    return true;
            return false;
        }

        static string TrimAccount(string json)
        {
            json = json.Remove(0, 24); //Initial part of the json string
            json = json.Remove(json.Length-4, 3); //Final }
            return json;
        }

        public static AppSettings ParseAppSettings(JObject obj)
        {
            try
            {
                AppSettings collection = new AppSettings();

                foreach (KeyValuePair<string, JToken> pair in obj)
                {
                    switch (pair.Key)
                    {
                        case "SteamID": collection.selectedSteamID = (string)pair.Value; break;
                        case "SteamPath": collection.selectedSteamPath = (string)pair.Value; break;
                            //Add other user settings here, as they are necessary
                    }
                }
                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
    