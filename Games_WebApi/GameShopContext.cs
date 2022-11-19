using Games_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi
{
    public static class GameShopContext
    {
        public static string connectionString;
        public static string urls;

        static GameShopContext()
        {
            connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING",EnvironmentVariableTarget.User);
            urls = Environment.GetEnvironmentVariable("URLS", EnvironmentVariableTarget.User);
            if(connectionString == null || urls == null)
            {
                throw new Exception("Ошибка получения перменных окружения") {
                    HelpLink= "Создайте config.bat вне проекта" +
                    "setx CONNECTION_STRING \"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = DataBase\\db1.mdb\"\n"+
                    "setx URLS http://192.168.43.192:33833"
                };
            }
        }
    }
}
