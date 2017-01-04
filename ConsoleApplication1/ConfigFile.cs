using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAR_Stat
{
    class ConfigFile
    {
        public static string ADDRESS { get; set; }
        public static int? START_PAGE { get; set; }
        public static int? END_PAGE { get; set; }
        public static int? LOG_LEVEL { get; set; }
        public static string SEPARATOR { get; set; }
        public static string FILE_OUTPUT { get; set; }
        public static bool LoadConfig()
        {
            try
            {
                ADDRESS = ConfigurationSettings.AppSettings["Address Start"];
                int tempInt = 0;
                START_PAGE = int.TryParse(ConfigurationSettings.AppSettings["Start Page"], out tempInt) ? tempInt : 0;
                END_PAGE = int.TryParse(ConfigurationSettings.AppSettings["End Page"], out tempInt) ? tempInt : 0;
                SEPARATOR = ConfigurationSettings.AppSettings["Separator"];
                FILE_OUTPUT = ConfigurationSettings.AppSettings["Output File"];
                return true;
            }
            catch(Exception exp)
            {
                Logger.PrintLC("Failed to load Settings from the configuration file.\nLoading the default settings instead.");
                ADDRESS = "";
                START_PAGE = 1;
                END_PAGE = 2000;
                return false;
            }
        }

    }
}
