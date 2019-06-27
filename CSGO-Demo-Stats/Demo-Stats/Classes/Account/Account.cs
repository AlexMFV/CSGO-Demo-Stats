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
        public string steamID { get; set; } //Steam User ID
        public string visibilityState { get; set; } //How other people see the account
        public string profileState { get; set; } //State of the profile, public, private, etc
        public string personaName { get; set; } //Nickname
        public string lastlogoff { get; set; } //Epoch of last log on
        public string commentPermission { get; set; } //Can other people comment on the profile?
        public string profileURL { get; set; } //URL of profile
        public string avatarFull { get; set; } //Link to the full url image
        public string personaState { get; set; } //IDK
        public string primaryClanID { get; set; } //ID of the group the user set as default
        public string timeCreated { get; set; } //Epoch of when the account was created
        public string personaStateFlags { get; set; } //IDK
        public string locCountryCode { get; set; } //Country code (eg. PT, UK)
        public string locStateCode { get; set; } //State id to find on the locations file
        public string locCityID { get; set; } //City id to find on the locations file

        public Account() { }
    }
}
