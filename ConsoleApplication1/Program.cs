using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAR_Stat
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigFile.LoadConfig();
            DataOps.ParseHTML("https://mwomercs.com/profile/leaderboards?page=1&type=0");
            
        }

    }
}
