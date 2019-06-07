using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoInfo;

namespace CSGO_Demo_Stats
{
    public partial class Form1 : Form
    {
        string fileContent = String.Empty;

        DemoParser parser;
        Demo demo = new Demo();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Parser_RoundStart(object sender, RoundStartedEventArgs e)
        {
            demo.NextRoundNumber();
            txtContent.Text += "\r\n Round " + demo.game_round + " Started! \r\n\r\n";
        }

        private void Parser_BombExploded(object sender, BombEventArgs e)
        {
            txtContent.Text += "Bomb Exploded! Round " + demo.game_round + "\r\n";
        }

        private void Parser_BombDefused(object sender, BombEventArgs e)
        {
            txtContent.Text += "Bomb Defused! Round " + demo.game_round + "\r\n";
        }

        private void Parser_BombPlanted(object sender, BombEventArgs e)
        {
            txtContent.Text += "Bomb Planted! Round " + demo.game_round + "\r\n";
        }

        private void Parser_PlayerKilled(object sender, PlayerKilledEventArgs e)
        {
            txtContent.Text += e.Killer.Name + " killed " + e.Victim.Name + "\r\n";
        }

        private void Parser_MatchStarted(object sender, MatchStartedEventArgs e)
        {
            txtContent.Text += "Game Started!" + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofdFile.Filter = "CSGO Demo File|*.dem";

            if(ofdFile.ShowDialog() == DialogResult.OK)
            {
                txtContent.Text = "";
                parser = new DemoParser(File.OpenRead(ofdFile.FileName));
                parser.ParseHeader();
                demo.Map = parser.Map;
                parser.RoundStart += Parser_RoundStart;
                parser.MatchStarted += Parser_MatchStarted;
                parser.MatchStarted += Parser_MatchStarted;
                parser.PlayerKilled += Parser_PlayerKilled;
                parser.BombPlanted += Parser_BombPlanted;
                parser.BombDefused += Parser_BombDefused;
                parser.BombExploded += Parser_BombExploded;
                parser.ParseToEnd();
            }
        }
    }
}
