using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebInstitution.Helpers
{
    public class ImgurUpload
    {
        public static string UploadImage(Image image)
        {
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + "1f6b47565c4ed78");
            System.Collections.Specialized.NameValueCollection Keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Keys.Add("image", Convert.ToBase64String( ms.ToArray() ));
                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image", Keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                string url = match.ToString().Replace("link\":\"", "").Replace("\"", "").Replace("\\/", "/");
                return url;
            }
            catch (Exception s)
            {
                return "Failed!";
            }
        }
    }

}