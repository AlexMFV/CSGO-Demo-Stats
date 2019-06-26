using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    class APIKey
    {
        static string _key = String.Empty;

        /// <summary>
        /// Grabs the provided API Key from the file in the Application Folder, if empty it returns null.
        /// Also Stores the APIKey on the object.
        /// </summary>
        /// <returns>API key in string format.</returns>
        public static string GetKeyFromFile()
        {
            try
            {
                StreamReader reader = new StreamReader(Path.GetFullPath(@"..\..\") + "/API/api_key.txt");
                _key = reader.ReadLine();
            }
            catch(Exception ex)
            {
                return null;
            }

            return _key;
        }
    }
}
