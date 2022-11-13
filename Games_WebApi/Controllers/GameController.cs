using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI_Core_Magerramov.Models;
using System.Data.OleDb;
using Games_WebApi.Controllers;

namespace WebAPI_Core_Magerramov.Controllers
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
                OleDbCommand oleDbCommand = new OleDbCommand("Select * from Games", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    games.Add(new Game()
                    {
                        ID = reader.GetInt32(0),
                        NameG = reader.GetString(1).Trim(),
                        Description = reader.GetString(2).Trim(),
                        Price = reader.GetDecimal(3),
                        Rating = reader.GetInt16(4)
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT Games count = {0}", games.Count));
            return games;
        }

        // GET: Game/5
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
                        Rating = reader.GetInt16(4)
                    };
                    oleDbConnection.Close();
                    return game;
                }
            }
            return NotFound();
        }

        // POST: Game/Insert
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
                    string.Format("INSERT INTO Games ( NameG, Description, Price, Rating )" +
                                  "VALUES ( '{0}', '{1}', {2}, {3})",
                                  game.NameG, game.Description, game.Price, game.Rating
                    ),
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] INSERT INTO Games ( NameG, Description, Price, Rating )" +
                                  "VALUES ( '{0}', '{1}', {2}, {3})",
                                  game.NameG, game.Description, game.Price, game.Rating));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // GET: Game/5
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
                                  "SET NameG = '{0}', Description = '{1}', Price = {2}, Rating = {3} " +
                                  "WHERE (ID = {4}) ",
                                  game.NameG, game.Description, game.Price.ToString().Replace(',', '.'), game.Rating, game.ID
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

        // GET: Game/5
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

        // GET: Game используется как заглушка
        public ActionResult Index()
        {
            return View();
        }
    }
}
