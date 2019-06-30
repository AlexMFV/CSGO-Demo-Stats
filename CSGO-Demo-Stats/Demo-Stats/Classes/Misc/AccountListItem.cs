using System.Windows.Media.Imaging;

namespace Demo_Stats
{
    public class AccountListItem
    {
        public string Name { get; set; }
        public string SteamID { get; set; }

        public AccountListItem(string _name, string _steamID)
        {
            this.Name = _name;
            this.SteamID = _steamID;
        }
    }
}
