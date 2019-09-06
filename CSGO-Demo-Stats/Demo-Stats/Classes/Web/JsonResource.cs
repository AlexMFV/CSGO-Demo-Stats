using Demo_Stats.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public static class JsonResource
    {
        /// <summary>
        /// Gets the json value from the Token specified, from a URL
        /// </summary>
        /// <param name="value_name">Token specified as a string</param>
        /// <param name="url">URL of the Json String</param>
        /// <returns>Returns the value in string format</returns>
        public static string GrabJSONValue(string value_name, string url)
        {
            try
            {
                string json = GrabJSONString(url);
                JObject objs = JObject.Parse(Parser.TrimAccount(JsonResource.GrabJSONString(url)));
                foreach (KeyValuePair<string, JToken> pair in objs)
                {
                    if (pair.Key == value_name)
                        return pair.Value.ToString();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<string> GrabJSONValue(string value_name, List<string> urls)
        {
            try
            {
                List<string> json = GrabJSONString(urls);
                List<string> imgs = new List<string>();

                foreach (string url in urls)
                {
                    JObject objs = JObject.Parse(Parser.TrimAccount(JsonResource.GrabJSONString(url)));
                    foreach (KeyValuePair<string, JToken> pair in objs)
                    {
                        if (pair.Key == value_name)
                            imgs.Add(pair.Value.ToString());
                    }
                }

                return imgs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the full JSON from a URL
        /// </summary>
        /// <param name="url">URL to retreive the JSON string</param>
        /// <returns>The JSON string</returns>
        public static string GrabJSONString(string url)
        {
            try
            {
                string _json;
                using (WebClient web = new WebClient())
                {
                    _json = web.DownloadString(url);
                }
                return _json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<string> GrabJSONString(List<string> urls)
        {
            try
            {
                List<string> _jsons = new List<string>();
                using (WebClient web = new WebClient())
                {
                    foreach (string url in urls)
                        _jsons.Add(web.DownloadString(url));
                }
                return _jsons;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the full JSON from a URL (Async)
        /// </summary>
        /// <param name="url">URL to retreive the JSON string</param>
        /// <returns>The JSON string</returns>
        public static async Task<string> GrabJSONStringAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage resp = await client.GetAsync(url);
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        string _json = await resp.Content.ReadAsStringAsync();
                        return _json;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GrabSteamJSONString(string id)
        {
            try
            {
                string _json;
                using (WebClient web = new WebClient())
                {
                    _json = web.DownloadString(SteamURL(id));
                }
                return _json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the full Steam User API url
        /// </summary>
        /// <returns></returns>
        public static string SteamURL()
        {
            string apiKey = APIKey.GetKeyFromFile();
            return Resources.API_ProfileURL1 + apiKey + Resources.API_ProfileURL2;
        }

        /// <summary>
        /// Returns the full Steam User API url with the specified SteamID
        /// </summary>
        /// <param name="steamID">SteamID of the Steam User</param>
        /// <returns>Full Steam API url</returns>
        public static string SteamURL(string steamID)
        {
            string apiKey = APIKey.GetKeyFromFile();
            return Resources.API_ProfileURL1 + apiKey + Resources.API_ProfileURL2 + steamID;
        }
    }
}
