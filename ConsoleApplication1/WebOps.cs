using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BBAR_Stat
{
    class WebOps
    {
        /// <summary>
        /// Download the entire HTML code of a page.
        /// </summary>
        /// <param name="theURL"></param>
        /// <returns></returns>
        public static string downloadWebPage(string theURL)
        {
            //### download a web page to a string
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Stream data = client.OpenRead(theURL);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            return s;
        }

        public static string LoginAndDownload()
        {
            string formUrl = "https://mwomercs.com/do/login"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
            string formParams = string.Format("email={0}&password={1}", "eregiongreenleafthegray@yahoo.it", "chupa33");
            string cookieHeader;

            WebRequest req = WebRequest.Create(formUrl);
            //var cookies = new CookieContainer();
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(formUrl);
            //req.CookieContainer = cookies;

            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            cookieHeader = resp.Headers["Set-cookie"];
            string pageFrom;
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                pageFrom = sr.ReadToEnd();
            }


            //var cookies = new CookieContainer();
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(formUrl);
            //req.CookieContainer = cookies;



            string pageSource;
            string getUrl = "https://mwomercs.com/profile";

            //WebRequest getRequest = WebRequest.Create(getUrl);
            //getRequest.Headers.Add("Cookie", cookieHeader);

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            getRequest.CookieContainer = new CookieContainer();
            getRequest.CookieContainer.Add(resp.Cookies);

            cookieHeader = "leaderboard__rank_by = 0; hl = en_us; _ga = GA1.2.1705776397.1474369677; PHPSESSID = 40hrr0kqqgrh4nbevnrltokmj3";
            getRequest.Headers.Add("Cookie", cookieHeader);
            cookieHeader = "keep-alive";
            getRequest.Headers.Add("Connection", cookieHeader);

            WebResponse getResponse = getRequest.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                pageSource = sr.ReadToEnd();
            }



            //HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
            //getRequest.CookieContainer = new CookieContainer();
            //getRequest.CookieContainer.Add(resp.Cookies);
            //getRequest.Headers.Add("Cookie", cookieHeader);
            return cookieHeader;
        }


        public static void GetPage()
        {
            //Run selenium
            //ChromeDriver cd = new ChromeDriver(@"chromedriver_win32");
            FirefoxDriver cd = new FirefoxDriver();
            cd.Url = @"https://mwomercs.com/login";
            cd.Navigate();
            IWebElement e = cd.FindElementById("email");
            e.SendKeys("eregiongreenleafthegray@yahoo.it");
            e = cd.FindElementById("password");
            e.SendKeys("chupa33");
            e = cd.FindElementByCssSelector("button.btn");
            //e = cd.FindElementByXPath(@"//a[contains(text(), 'Sign in') or contains(text(), 'sign in')]");
            //e = cd.FindElementByXPath(@"//*[@id=""main""]/div/div/div[2]/table/tbody/tr/td[1]/div/form/fieldset/table/tbody/tr[6]/td/button");
            e.Click();

            List<PlayerStatT> players = new List<PlayerStatT>();

            long tempLong = 0;
            int tempInt = 0;
            double tempDouble = 0;

            
            for (int? page = ConfigFile.START_PAGE; page < ConfigFile.END_PAGE; page++)
            {
                cd.Url = @"https://mwomercs.com/profile/leaderboards?page=" + page + "&type=0";
                cd.Navigate();
                for (int x = 1; x < 21; x++)
                {
                    PlayerStatT player = new PlayerStatT();
                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(1)");
                    long.TryParse(e.Text, out tempLong);
                    player.Rank = tempLong;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(2)");
                    player.Name = e.Text;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(3)");
                    int.TryParse(e.Text, out tempInt);
                    player.Wins = tempInt;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(4)");
                    int.TryParse(e.Text, out tempInt);
                    player.Losses = tempInt;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(5)");
                    double.TryParse(e.Text.Replace(".", ","), out tempDouble);
                    player.WLr = tempDouble;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(6)");
                    int.TryParse(e.Text, out tempInt);
                    player.Kills = tempInt;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(7)");
                    int.TryParse(e.Text, out tempInt);
                    player.Deaths = tempInt;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(8)");
                    double.TryParse(e.Text.Replace(".", ","), out tempDouble);
                    player.KDr = tempDouble;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(9)");
                    int.TryParse(e.Text, out tempInt);
                    player.GamesPlayed = tempInt;

                    e = cd.FindElementByCssSelector(".table > tbody:nth-child(2) > tr:nth-child(" + x + ") > td:nth-child(10)");
                    int.TryParse(e.Text, out tempInt);
                    player.AvarageMatchScore = tempInt;
                    WriteLine(player);
                    players.Add(player);
                }
            } 



            //Get the cookies
            foreach (OpenQA.Selenium.Cookie c in cd.Manage().Cookies.AllCookies)
            {
                string name = c.Name;
                string value = c.Value;
                
                //cd.Add(new System.Net.Cookie(name, value, c.Path, c.Domain));
            }

            //Fire off the request
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create("https://fif.com/components/com_fif/tools/capacity/values/");
            //hwr.CookieContainer = cc;
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencoded";
            StreamWriter swr = new StreamWriter(hwr.GetRequestStream());
            swr.Write("feeds=35");
            swr.Close();

            WebResponse wr = hwr.GetResponse();
            string s = new System.IO.StreamReader(wr.GetResponseStream()).ReadToEnd();
        }

        public static bool WriteLine(PlayerStatT player)
        {
            string valore = player.Rank + ConfigFile.SEPARATOR +
                            player.Name + ConfigFile.SEPARATOR +
                            player.Wins + ConfigFile.SEPARATOR +
                            player.Losses + ConfigFile.SEPARATOR +
                            player.WLr + ConfigFile.SEPARATOR +
                            player.Kills + ConfigFile.SEPARATOR +
                            player.Deaths + ConfigFile.SEPARATOR +
                            player.KDr + ConfigFile.SEPARATOR +
                            player.GamesPlayed + ConfigFile.SEPARATOR +
                            player.AvarageMatchScore + ConfigFile.SEPARATOR;
            Logger.PrintF(ConfigFile.FILE_OUTPUT, valore, false);
            return true;
        }

    }
}
