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
        public string _steamid { get; set; } //Steam User ID
        public string _personaname { get; set; } //Nickname
        public string _lastlogoff { get; set; } //Epoch of last log on
        public string _profileurl { get; set; } //URL of profile
        public string _avatarfull { get; set; } //Link to the full url image
        public string _timecreated { get; set; } //Epoch of when the account was created
        public string _loccountrycode { get; set; } //Country code (eg. PT, UK)
        public string _locstatecode { get; set; } //State id to find on the locations file
        public string _loccityid { get; set; } //City id to find on the locations file

        public Account() { }
    }
}
