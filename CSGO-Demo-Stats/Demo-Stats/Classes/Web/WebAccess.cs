using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        ////Maybe DELETE
        public static List<BitmapImage> GetPlayerImages(Players players)
        {
            try
            {
                List<BitmapImage> imgs = new List<BitmapImage>();
                List<string> urls = new List<string>();
                string mainUrl = JsonResource.SteamURL();

                foreach (Player p in players)
                    urls.Add(mainUrl + p.steamID);

                List<string> imgUrl = JsonResource.GrabJSONValue("avatar", urls);

                using (WebClient client = new WebClient())
                {
                    foreach (string url in imgUrl)
                    {
                        imgs.Add(ImageFromBuffer(client.DownloadData(url)));
                    }
                    return imgs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ////Maybe DELETE
        public static BitmapImage ImageFromBuffer(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
