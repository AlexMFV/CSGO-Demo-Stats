using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    class Players : CollectionBase
    {
        public void Add(Player player)
        {
            List.Add(player);
        }

        public void Remove(Player player)
        {
            List.Remove(player);
        }

        public Player this[int index]
        {
            get { return (Player)List[index]; }
            set { List[index] = value; }
        }
    }
}
