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
                    _json = web.DownloadString(Resources.API_ProfileURL + id);
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

        public static void ParseAccount(string id)
        {
            JObject objs = JObject.Parse(TrimAccount(GrabJSONString(id)));
            foreach (KeyValuePair<string, JToken> pair in objs)
            {

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
    