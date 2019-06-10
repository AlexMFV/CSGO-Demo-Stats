using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Demo_Stats
{
    public static class Parser
    {
        //REMOVE THIS and add to the Resources later (Testing purposes only!)
        static string _steamAPIKey = "AA741B7FC2EE534447387D322EBEDC39";
        static string _MySteamID = "76561198061445771";
        static string _getPlayerURL = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" +
            "AA741B7FC2EE534447387D322EBEDC39" +
            "&steamids=";

        static string GrabJSONString(string id)
        {
            try
            {
                string _json;

                using (WebClient web = new WebClient())
                {
                    _json = web.DownloadString(_getPlayerURL + id);
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
            return JsonConvert.DeserializeObject<Account>(TrimAccount(GrabJSONString(id)));
        }

        static string TrimAccount(string json)
        {
            json = json.Remove(0, 24); //Initial part of the json string
            json = json.Remove(json.Length-4, 3); //Final }
            return json;
        }
    }
}
