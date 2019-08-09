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
            if (Directory.Exists(path))
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
                                newDemo.source = ParseDemoSource(parser.Header.ServerName);
                                newDemo.map = parser.Map;
                                newDemo.date = GetDataFromDemoInfo(filename);
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
            }

            return demos;
        }

        private static string GetDataFromDemoInfo(string path)
        {
            try
            {
                if (File.Exists(path + ".info"))
                {
                    using (FileStream file = File.OpenRead(path + ".info"))
                    {
                        CDataGCCStrike15_v2_MatchInfo matchinfo = Serializer.Deserialize<CDataGCCStrike15_v2_MatchInfo>(file);
                        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        return epochStart.AddSeconds(matchinfo.matchtime).ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
                    }
                }
                else
                {
                    //If the info file does not exist, just return the last time the file was modified
                    FileInfo info = new FileInfo(path);
                    return info.LastWriteTimeUtc.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
                }
            }
            catch(Exception ex)
            {
                //Needs to be treated
                return null;
            }
        }

        private static Source ParseDemoSource(string hostname)
        {
            if (hostname.Contains("Valve CS:GO"))
                return Source.Valve;

            if (hostname.Contains("FACEIT.com"))
                return Source.Faceit;

            if (hostname.Contains("Esportal.com"))
                return Source.Esportal;

            if (hostname.Contains("Counter-Strike")) //Most probably a ESEA Demo
                return Source.Esea;

            //PopFlash Demo

            //CEVO Demo

            return Source.Unknown;
        }

        private static string ConvertDuration(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"hh\:mm\:ss");
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
