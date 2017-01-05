using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BBAR_Stat
{
    class DataOps
    {
        public static string ParseHTML (string html)
        {

            string ret = @"< span > QUICK PLAY LEADERBOARD</ span >
   </ h2 >
   


   < div style = ""margin: 0px 15px 15px 15px;"" >
    

        < table class=""table table-striped"">
	
		<thead>
		
			<tr>
			
				<th width = ""8%"" > Rank </ th >

                < th width=""30%"">Pilot Name</th>
				<th width = ""8%"" > Total Wins</th>
 				<th width = ""8%"" > Total Losses</th>
 				<th width = ""7%"" > W / L Ratio</th>
 				<th width = ""8%"" > Total Kills</th>
 				<th width = ""8%"" > Total Deaths</th>
 				<th width = ""8%"" > K / D Ratio</th>
 				<th width = ""6%"" > Games Played</th>
 				<th width = ""15%"" > Average Match Score</th>
			
			</tr>
		
		</thead>
		
		<tbody>";

            string actualPage; //= WebOps.downloadWebPage(html);
            //WebOps.GetPage();
            actualPage = WebOps.LoginAndDownload();
            actualPage = WebOps.downloadWebPage(html);
            HtmlPage test = new HtmlPage();
            test.Body = actualPage;
            test.PageName = html;
            string finalXLM = DataOps.GetXMLFromObject(test);

            string search = @"";
            Logger.PrintF(@"C:\ROOTtest\webtest.txt", actualPage, false);
            int pFrom = actualPage.IndexOf("key : ") + "key : ".Length;
            int pTo = actualPage.LastIndexOf(" - ");

            string result = actualPage.Substring(pFrom, pTo - pFrom);

            return ret;
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
    }
}
