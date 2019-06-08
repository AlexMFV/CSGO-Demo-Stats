using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_Demo_Stats
{
    public class Demo
    {
        #region Variables

        public string demo_map;
        public int game_round = 0;

        #endregion

        #region Methods

        //Constructor
        public Demo() { }

        /// <summary>
        /// Gets or Sets the map of the current demo
        /// </summary>
        public string Map
        {
            get { return demo_map; }
            set { demo_map = value; }
        }

        /// <summary>
        /// Gets the number of the current round of the demo.
        /// </summary>
        public int CurrentRound
        {
            get { return game_round; }
        }

        #endregion

        #region Variable Manipulators

        /// <summary>
        /// Adds to the round number (usually called when a new round starts)
        /// </summary>
        public void NextRoundNumber()
        {
            this.game_round++;
        }

        #endregion
    }
}
