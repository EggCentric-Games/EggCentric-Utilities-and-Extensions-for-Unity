using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

namespace EggCentric.Networking
{
    public static class NetworkingUtilities
    {
        public static string GetUserID()
        {
            string macAdress = null;

            foreach (NetworkInterface ninf in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ninf.Description == "en0")
                {
                    macAdress = ninf.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    macAdress = ninf.GetPhysicalAddress().ToString();

                    if (macAdress != "")
                        break;
                }
            }
            if (macAdress == "")
                macAdress = SystemInfo.deviceUniqueIdentifier;

            return macAdress;
        }

        public static bool IsConnected()
        {
            string HtmlText = GetHtmlFromUrl("http://google.com");
            if (HtmlText == "")
            {
                return false;
            }
            else
            {
                if (!HtmlText.Contains("schema.org/WebPage"))
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        public static string GetHtmlFromUrl(string resource)
        {
            string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                    if (isSuccess)
                    {
                        using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                        {
                            //We are limiting the array to 80 so we don't have
                            //to parse the entire html document feel free to 
                            //adjust (probably stay under 300)
                            char[] cs = new char[80];
                            reader.Read(cs, 0, cs.Length);
                            foreach (char ch in cs)
                            {
                                html += ch;
                            }
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return html;
        }
    }
}
