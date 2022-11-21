using Games_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Games_WebApi.Controllers
{
    //api/User
    [Route("api/[controller]")]
    [ApiController]

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public class UserController : Controller
    {
        [HttpGet]
        public List<User> Get()
        {
            List<User> users = new List<User>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand("Select * from Users", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User()
                    {
                        ID = reader.GetInt32(0),
                        Login = reader.GetString(1).Trim(),
                        Password = reader.GetString(2).Trim(),
                        UserType = reader.GetInt32(3)
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine(string.Format("[D] SELECT User count = {0}", users.Count));
            return users;
        }

        // GET: User/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById(int id)
        {
            User user;
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("SELECT * FROM Users WHERE ID = {0}", id), oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        ID = reader.GetInt32(0),
                        Login = reader.GetString(1).Trim(),
                        Password = reader.GetString(2).Trim(),
                        UserType = reader.GetInt32(3)
                    };
                    oleDbConnection.Close();
                    return user;
                }
            }
            return NotFound();
        }

        // POST: User/Insert
        [HttpPost]
        public ActionResult Insert([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    string.Format("INSERT INTO Users ( Login, [Password], UserType )" +
                                  "VALUES ( '{0}', '{1}', {2})",
                                  user.Login, user.Password, user.UserType
                    ),
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] INSERT INTO Users ( Login, [Password], UserType )" +
                                  "VALUES ( '{0}', '{1}', {2})",
                                  user.Login, user.Password, user.UserType));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // GET: User/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    string.Format("UPDATE Users " +
                                  "SET Login = '{0}', [Password] = '{1}', UserType = {2} "+
                                  "WHERE (ID = {3}) ",
                                  user.Login, user.Password, user.UserType, user.ID
                    ),
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] UPDATE Users ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        // GET: User/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("DELETE * FROM Users WHERE ID = {0}", id), oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] DELETE Users ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // GET: User используется как заглушка
        public ActionResult Index()
        {
            return View();
        }

    }
}
