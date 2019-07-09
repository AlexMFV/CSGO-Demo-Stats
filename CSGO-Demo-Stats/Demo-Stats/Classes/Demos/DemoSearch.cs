using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInfo;
using ProtoBuf;

namespace Demo_Stats
{
    public class DemoSearch
    {
        
        /// <summary>
        /// Searches in the selected folder for new demos
        /// </summary>
        /// <param name="idx">Index of the folder</param>
        /// <param name="col">Collection of folder paths</param>
        /// <returns>Returns a collection of Demos</returns>
        public static Demos SearchNewDemos(int idx, Folders col, Demos demos)
        {
             return RetrieveDemosFromFolder(col[idx], demos);
        }

        /// <summary>
        /// Searches in all the possible folders for new demos
        /// </summary>
        /// <param name="col">Collection of folder paths</param>
        /// <returns>Returns a Collection of Demos</returns>
        public static Demos SearchNewDemos(Folders col, Demos demos)
        {
            foreach (string folder in col)
                demos = RetrieveDemosFromFolder(folder, demos);

            return demos;
        }

        private static Demos RetrieveDemosFromFolder(string path, Demos demos)
        {
            foreach (string filename in Directory.GetFiles(path))
            {
                if (!filename.Contains(".info") && !filename.Contains(".vdm"))
                {
                    using (FileStream file = new FileStream(filename, FileMode.Open))
                    {
                        Demo newDemo = new Demo();
                        DemoParser parser = new DemoParser(file);
                        parser.ParseHeader();

                        string demoName = filename.Replace(path + "\\", "");

                        if (!DemoExists(demoName, demos))
                        {
                            newDemo.name = demoName;
                            newDemo.source = "Valve"; //HARDCODED
                            newDemo.map = parser.Map;
                            newDemo.date = GetDataFromDemoInfo(filename + ".info"); //HARDCODED
                            newDemo.demo_client = parser.Header.ClientName;
                            newDemo.hostname = parser.Header.ServerName;
                            newDemo.duration = ConvertDuration((int)parser.Header.PlaybackTime);
                            newDemo.server_tickrate = (int)(parser.Header.PlaybackTicks / parser.Header.PlaybackTime);
                            newDemo.demo_framerate = (int)Math.Ceiling(parser.TickRate);
                            newDemo.demo_ticks = parser.Header.PlaybackTicks;
                            demos.Add(newDemo);
                        }
                    }
                }
            }

            return demos;
        }

        private static string GetDataFromDemoInfo(string path)
        {
            using (FileStream file = File.OpenRead(path))
            {
                CDataGCCStrike15_v2_MatchInfo matchinfo = Serializer.Deserialize<CDataGCCStrike15_v2_MatchInfo>(file);
                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epochStart.AddSeconds(matchinfo.matchtime).ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
            }
        }

        private static string ConvertDuration(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString("HH:mm:ss");
        }

        private static bool DemoExists(string name, Demos demos)
        {
            if (demos.Count > 0)
            {
                foreach (Demo demo in demos)
                    if (demo.name == name)
                        return true;
            }
            return false;
        }
    }
}
