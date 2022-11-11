using Games_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Core_Magerramov.Models;

namespace Games_WebApi.Controllers
{
    public static class GameShopContext
    {
        //String connectionString = @"Data Source=com-234-2\sqlexpress;Initial Catalog=DB_Magerramov;Integrated Security=True";
        //public static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb;";
        public static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db1.mdb";
    }
}
