﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public class Demo
    {
        #region Variables

        //Initial Values
        public string source { get; set; } //Valve, ESEA, Faceit, etc
        public string name { get; set; }
        public string date { get; set; }
        public string map { get; set; }
        public string duration { get; set; }
        public string hostname { get; set; }
        public string demo_client { get; set; } //GOTV Demo
        public int server_tickrate { get; set; }
        public int demo_framerate { get; set; }
        public int demo_ticks { get; set; }

        //Other Values
        public string team1_name { get; set; }
        public string team2_name { get; set; }
        public string status { get; set; }
        public int score_t1 { get; set; }
        public int score_t2 { get; set; }
        public int bans { get; set; }
        public double htlv_rating { get; set; }
        public double esea_rws { get; set; } //Round Win Share
        public int k1 { get; set; }
        public int k2 { get; set; }
        public int k3 { get; set; }
        public int k4 { get; set; }
        public int k5 { get; set; }
        public int kills { get; set; }
        public int trade_kills { get; set; }
        public int jump_kills { get; set; }
        public int crouch_kills { get; set; }
        public double avg_damage_round { get; set; }
        public double avg_kills_round { get; set; }
        public double avg_deaths_round { get; set; }
        public double avg_assists_round { get; set; }
        public int total_damage_health { get; set; }
        public int total_damage_armor { get; set; }
        public int clutches_won { get; set; }
        public int bombs_defused { get; set; }
        public int bombs_exploded { get; set; }
        public int bombs_planted { get; set; }
        public string comment { get; set; }

        //For now i'll just leave these variables, later on i'll need to add, rounds,
        //players, utilities, kills, deaths, locations, scores, hs%, kdr, hltv rating, etc

        #endregion

        #region Methods

        //Constructor
        public Demo() { }

        /// <summary>
        /// Creates a demo object with the initial values (when it's gathered as a new demo)
        /// </summary>
        /// <param name="_source">Source of the Demo (Valve, Faceit, etc)</param>
        /// <param name="_name">Name of the demo</param>
        /// <param name="_date">Date of the demo</param>
        /// <param name="_map">Map played in the demo</param>
        /// <param name="_duration">Duration of the demo in <minutes>:<seconds></param>
        /// <param name="_hostname">The hostname of the demo (with ip address)</param>
        /// <param name="_demo_client">Client of the demo (GOTV)</param>
        /// <param name="_server_tickrate">The tickrate of the server</param>
        /// <param name="_framerate">Framerate of the demo</param>
        /// <param name="_ticks">Number of total ticks of the demo</param>
        public Demo(string _source, string _name, string _date, string _map, string _duration, string _hostname,
            string _demo_client, int _server_tickrate, int _framerate, int _ticks)
        {
            source = _source;
            name = _name;
            date = _date;
            map = _map;
            duration = _duration;
            hostname = _hostname;
            demo_client = _demo_client;
            server_tickrate = _server_tickrate;
            demo_framerate = _framerate;
            demo_ticks = _ticks;
        }

        #endregion

        #region Variable Manipulators

        /// <summary>
        /// Adds to the round number (usually called when a new round starts)
        /// </summary>
        public void NextRoundNumber()
        {
            //this.Round++;
        }

        #endregion
    }
}
