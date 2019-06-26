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

                //Verify if JSON string is complete, in case the user entered a wrong ID

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
                        case "steamid": newAcc._steamID = (string)pair.Value; break;
                        case "communityvisibilitystate": newAcc._visibilityState = (string)pair.Value; break;
                        case "profilestate": newAcc._profileState = (string)pair.Value; break;
                        case "personaname": newAcc._personaName = (string)pair.Value; break;
                        case "lastlogoff": newAcc._lastlogoff = (string)pair.Value; break;
                        case "commentpermission": newAcc._commentPermission = (string)pair.Value; break;
                        case "profileurl": newAcc._profileURL = (string)pair.Value; break;
                        case "avatarfull": newAcc._avatarFull = (string)pair.Value; break;
                        case "personastate": newAcc._personaState = (string)pair.Value; break;
                        case "primaryclanid": newAcc._primaryClanID = (string)pair.Value; break;
                        case "timecreated": newAcc._timeCreated = (string)pair.Value; break;
                        case "personastateflags": newAcc._personaStateFlags = (string)pair.Value; break; 
                        case "loccountrycode": newAcc._locCountryCode = (string)pair.Value; break;
                        case "locstatecode": newAcc._locStateCode = (string)pair.Value; break;
                        case "loccityid": newAcc._locCityID = (string)pair.Value; break;
                    } 
                }
                return newAcc;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        static string TrimAccount(string json)
        {
            json = json.Remove(0, 24); //Initial part of the json string
            json = json.Remove(json.Length-4, 3); //Final }
            return json;
        }
    }
}
    