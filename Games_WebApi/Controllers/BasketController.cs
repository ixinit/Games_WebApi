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
    public class BasketController : Controller
    {

        [HttpGet] 
        public List<Basket> GetAll()
        {
            List<Basket> baskets = new List<Basket>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    $"SELECT * FROM Baskets",
                    oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    baskets.Add(new Basket()
                    {
                        ID = reader.GetInt32(0),
                        GameID = reader.GetInt32(1),
                        Count = reader.GetInt32(2),
                        UserID = reader.GetInt32(3),

                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT * Baskets count = {0}", baskets.Count));
            return baskets;
        }

        [HttpGet]
        [Route("owner/{id}")]
        public List<Basket> Get(int id)
        {
            List<Basket> baskets = new List<Basket>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    $"SELECT * FROM Baskets WHERE UserID = {id}",
                    oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    baskets.Add(new Basket()
                    {
                        ID = reader.GetInt32(0),
                        GameID = reader.GetInt32(1),
                        Count = reader.GetInt32(2),
                        UserID = reader.GetInt32(3),
                    
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine($"[D] SELECT Baskets owner by UserID = {id} count = {baskets.Count}");
            return baskets;
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Basket basket)
        {
            //"UPDATE Games SET Games.NameG = "2fgd", Games.Description = "3chv", Games.Price = 4.1, Games.Rating = 5 WHERE(((Games.ID) = 1))"
            if (basket == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    string.Format("UPDATE Baskets " +
                                  "SET GamedID = {0}, [Count] = {1}, UserID = {2}" +
                                  "WHERE (ID = {3}) ",
                                  basket.GameID, basket.Count, basket.UserID, basket.ID
                    ),
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] UPDATE Baskets ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("DELETE * FROM Baskets WHERE ID = {0}", id), oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] DELETE Baskets ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        [HttpPost]
        public ActionResult Insert([FromBody] Basket basket)
        {
            if (basket == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand checkExist = new OleDbCommand($"SELECT * FROM Baskets WHERE UserID = {basket.UserID} AND GameID = {basket.GameID}", oleDbConnection);
                OleDbDataReader reader = checkExist.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    OleDbCommand oleDbCommand = new OleDbCommand(
                        "UPDATE Baskets " +
                        $"SET [Count] = {reader.GetInt32(2)+1} "+
                        $"WHERE (ID = {reader.GetInt32(0)}) ",
                        oleDbConnection);

                    if (oleDbCommand.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("[D] UPDATE Baskets - ok");
                        return new OkResult();
                    }
                }
                else
                {
                    OleDbCommand oleDbCommand = new OleDbCommand(
                        "INSERT INTO Baskets ( GameID, [Count], UserID ) " +
                        $"VALUES ( {basket.GameID}, {basket.Count}, {basket.UserID} )",
                        oleDbConnection);

                    Console.WriteLine("[D]Try INSERT INTO Baskets ( GameID, [Count], UserID ) " +
                                      $"VALUES ( {basket.GameID}, {basket.Count}, {basket.UserID} )");

                    if (oleDbCommand.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("[D] INSERT INTO Baskets - ok");
                        return new OkResult();
                    }
                }
            }
            return new NoContentResult();
        }
    }
}
