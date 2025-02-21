using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBSetup
{
    public class ConnectToDataBase
    {
        public static string GetConnectionString()
        {
            ConfigurationManager configuration = new();
            configuration.AddJsonFile("appsettings.json");
            return configuration.GetConnectionString("default")
                ?? throw new Exception("Connection to DataBase is Lost!!!");
        }
    }
}
