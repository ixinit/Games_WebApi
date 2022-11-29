using Games_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Games_WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public class UserBasketController : Controller
    {
        [HttpGet]
        public List<ExtBasket> Get()
        {
            List<ExtBasket> baskets = new List<ExtBasket>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    "SELECT Baskets.ID, Games.NameG, Baskets.[Count], Games.Price*Baskets.[Count]" +
                    "AS TotalPrice FROM Baskets INNER JOIN Games ON Baskets.GameID=Games.ID;",
                    oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    baskets.Add(new ExtBasket()
                    {
                        ID = reader.GetInt32(0),
                        NameG = reader.GetString(1).Trim(),
                        Count = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT Baskets count = {0}", baskets.Count));
            return baskets;
        }
        [HttpGet]
        [Route("owner/{id}")]
        public List<ExtBasket> Get(int id)
        {
            List<ExtBasket> baskets = new List<ExtBasket>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    "SELECT Baskets.ID, Games.NameG, Baskets.[Count], Games.Price*Baskets.[Count] " +
                    "AS TotalPrice FROM Baskets INNER JOIN Games ON Baskets.GameID=Games.ID " +
                    $"WHERE Baskets.UserID = {id};",
                    oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    baskets.Add(new ExtBasket()
                    {
                        ID = reader.GetInt32(0),
                        NameG  = reader.GetString(1).Trim(),
                        Count = reader.GetInt32(2),
                        TotalPrice = reader.GetDecimal(3),

                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT Games count = {0}", baskets.Count));
            return baskets;
        }
    }
}
