using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInfo;

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
                            newDemo.date = DateTime.Now.ToShortDateString(); //HARDCODED
                            newDemo.demo_client = parser.Header.ClientName;
                            newDemo.hostname = parser.Header.ServerName;
                            newDemo.duration = ConvertDuration((int)parser.Header.PlaybackTime);
                            newDemo.server_tickrate = ((int)(parser.Header.PlaybackTicks / parser.Header.PlaybackTime)).ToString();
                            newDemo.demo_framerate = Math.Ceiling(parser.TickRate).ToString();
                            newDemo.demo_ticks = parser.Header.PlaybackTicks.ToString();
                            demos.Add(newDemo);
                        }
                    }
                }
            }

            return demos;
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
