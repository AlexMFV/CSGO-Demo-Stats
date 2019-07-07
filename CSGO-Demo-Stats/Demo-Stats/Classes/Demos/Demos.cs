using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    public class Demos : CollectionBase
    {
        public void Add(Demo demo)
        {
            List.Add(demo);
        }

        public void Remove(Demo demo)
        {
            List.Remove(demo);
        }

        public Demo this[int index]
        {
            get { return (Demo)List[index]; }
            set { List[index] = value; }
        }
    }
}
