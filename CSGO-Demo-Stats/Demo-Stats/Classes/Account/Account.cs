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
        public string _steamID { get; set; } //Steam User ID
        public string _visibilityState { get; set; } //How other people see the account
        public string _profileState { get; set; } //State of the profile, public, private, etc
        public string _personaName { get; set; } //Nickname
        public string _lastlogoff { get; set; } //Epoch of last log on
        public string _commentPermission { get; set; } //Can other people comment on the profile?
        public string _profileURL { get; set; } //URL of profile
        public string _avatarFull { get; set; } //Link to the full url image
        public string _personaState { get; set; } //IDK
        public string _primaryClanID { get; set; } //ID of the group the user set as default
        public string _timeCreated { get; set; } //Epoch of when the account was created
        public string _personaStateFlags { get; set; } //IDK
        public string _locCountryCode { get; set; } //Country code (eg. PT, UK)
        public string _locStateCode { get; set; } //State id to find on the locations file
        public string _locCityID { get; set; } //City id to find on the locations file

        public Account() { }
    }
}
