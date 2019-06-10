using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Demo_Stats
{
    public class Account
    {
        public string steamid { get; set; } //Steam User ID
        public string personaname { get; set; } //Nickname
        public string lastlogoff { get; set; } //Epoch of last log on
        public string profileurl { get; set; } //URL of profile
        public string avatarfull { get; set; } //Link to the full url image
        public string timecreated { get; set; } //Epoch of when the account was created
        public string loccountrycode { get; set; } //Country code (eg. PT, UK)
        public string locstatecode { get; set; } //State id to find on the locations file
        public string loccityid { get; set; } //City id to find on the locations file

        public Account() { }
    }
}
