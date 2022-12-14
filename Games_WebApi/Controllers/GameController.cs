using Games_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Games_WebApi.Controllers
{
    //api/Game
    [Route("api/[controller]")]
    [ApiController]

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public class GameController : Controller
    {
        // api/Game
        [HttpGet]
        public List<Game> Get()
        {
            List<Game> games = new List<Game>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand("Select * from Games ORDER BY ID", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    games.Add(new Game()
                    {
                        ID = reader.GetInt32(0),
                        NameG = reader.GetString(1).Trim(),
                        Description = reader.GetString(2).Trim(),
                        Price = reader.GetDecimal(3),
                        Rating = reader.GetInt16(4),
                        OwnerID = reader.GetInt32(5)
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT Games count = {0}", games.Count));
            return games;
        }

        // GET: api/Game/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Game> GetById(int id)
        {
            Game game;
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("SELECT * FROM Games WHERE ID = {0}", id), oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                if (reader.Read())
                {
                    game = new Game()
                    {
                        ID = reader.GetInt32(0),
                        NameG = reader.GetString(1).Trim(),
                        Description = reader.GetString(2).Trim(),
                        Price = reader.GetDecimal(3),
                        Rating = reader.GetInt16(4),
                        OwnerID = reader.GetInt32(5)
                    };
                    oleDbConnection.Close();
                    return game;
                }
            }
            return NotFound();
        }

        // api/Game/owner/1
        [HttpGet]
        [Route("owner/{id}")]
        public List<Game> Get(int id)
        {
            List<Game> games = new List<Game>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand($"Select * from Games WHERE OwnerID = {id}", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    games.Add(new Game()
                    {
                        ID = reader.GetInt32(0),
                        NameG = reader.GetString(1).Trim(),
                        Description = reader.GetString(2).Trim(),
                        Price = reader.GetDecimal(3),
                        Rating = reader.GetInt16(4),
                        OwnerID = reader.GetInt32(5)
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT Games count = {0}", games.Count));
            return games;
        }

        // POST: api/Game
        [HttpPost]
        public ActionResult Insert([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    string.Format("INSERT INTO Games ( NameG, Description, Price, Rating, OwnerID)" +
                                  "VALUES ( '{0}', '{1}', {2}, {3}, {4} )",
                                  game.NameG, game.Description, game.Price.ToString().Replace(",", "."), game.Rating, game.OwnerID
                    ),
                    oleDbConnection);
                Console.WriteLine(string.Format("[D]TRY INTO Games ( NameG, Description, Price, Rating, OwnerID)" +
                                  "VALUES ( '{0}', '{1}', {2}, {3}, {4} )",
                                  game.NameG, game.Description, game.Price.ToString().Replace(",", "."), game.Rating, game.OwnerID
                    ));
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("[D] INSERT INTO Games - ok");
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Game game)
        {
            //"UPDATE Games SET Games.NameG = "2fgd", Games.Description = "3chv", Games.Price = 4.1, Games.Rating = 5 WHERE(((Games.ID) = 1))"
            if (game == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    string.Format("UPDATE Games " +
                                  "SET NameG = '{0}', Description = '{1}', Price = {2}, Rating = {3}, OwnerID = {4} " +
                                  "WHERE (ID = {5}) ",
                                  game.NameG, game.Description, game.Price.ToString().Replace(',', '.'), game.Rating, game.OwnerID, game.ID
                    ),
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] UPDATE Games ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("DELETE * FROM Games WHERE ID = {0}", id), oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] DELETE Games ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // GET: api/Game используется как заглушка
        public ActionResult Index()
        {
            return View();
        }
    }
}
