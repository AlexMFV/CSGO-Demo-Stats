using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Demo_Stats
{
    public class WebAccess
    {
        public static BitmapImage GetImageFromURL(string url)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
