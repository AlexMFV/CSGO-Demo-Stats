using System.Collections;

namespace Demo_Stats
{
    class Accounts : CollectionBase
    {
        public void Add (Account acc)
        {
            List.Add(acc);
        }

        public void Remove(Account acc)
        {
            List.Remove(acc);
        }

        public Account this[int index]
        {
            get { return (Account)List[index]; }
            set { List[index] = value; }
        }
    }
}
