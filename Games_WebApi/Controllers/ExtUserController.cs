using Games_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi.Controllers
{
    //api/ExtUser
    [Route("api/[controller]")]
    [ApiController]

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public class ExtUserController : Controller
    {
        // GET: ExtUser/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ExtUser> GetById(int id)
        {
            ExtUser extuser;
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("SELECT * FROM Users WHERE ID = {0}", id), oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                if (reader.Read())
                {
                    OleDbCommand getTypeCommand = new OleDbCommand(string.Format("SELECT * FROM UserTypes WHERE ID = {0}", reader.GetInt32(3)), oleDbConnection);
                    OleDbDataReader typeReader = getTypeCommand.ExecuteReader();
                    if (typeReader.Read())
                    {
                        extuser = new ExtUser()
                        {
                            ID = reader.GetInt32(0),
                            Login = reader.GetString(1).Trim(),
                            Password = reader.GetString(2).Trim(),
                            UserType = new UserType()
                            {
                                ID = typeReader.GetInt32(0),
                                TypeName = typeReader.GetString(1).Trim(),
                                Read = typeReader.GetBoolean(2),
                                Create = typeReader.GetBoolean(3),
                                Edit = typeReader.GetBoolean(4),
                                EditAll = typeReader.GetBoolean(5),
                                Delete = typeReader.GetBoolean(6),
                                DeleteAll = typeReader.GetBoolean(7),
                                RezTable = typeReader.GetBoolean(8),
                                EmergTable = typeReader.GetBoolean(9)
                            }

                        };
                        oleDbConnection.Close();
                        //Console.WriteLine(user as User is User);
                        return extuser;
                    }
                }
            }
            return NotFound();
        }
    }
}
