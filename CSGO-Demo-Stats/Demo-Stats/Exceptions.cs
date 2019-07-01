using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Stats
{
    [Serializable()]
    public class InvalidSteamID : Exception
    {
        public InvalidSteamID() : base(String.Format("The entered SteamID64 is invalid," +
            " please enter a new one and try again!")) { }

        public InvalidSteamID(string SteamID) : base(String.Format("\"" + SteamID + "\" is not a invalid SteamID64," +
            " please enter a new one and try again!")) { }
    }

    public class IncorrectSteamID : Exception
    {
        public IncorrectSteamID() : base(String.Format("The entered SteamID64 does not exist," +
            " please enter a new one and try again!"))
        { }
    }
}
