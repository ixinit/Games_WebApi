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
    //api/UserType
    [Route("api/[controller]")]
    [ApiController]

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
    public class UserTypeController : Controller
    {
        //GET: api/UserType
        [HttpGet]
        public List<UserType> Get()
        {
            List<UserType> utypes = new List<UserType>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM UserTypes", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    utypes.Add(new UserType()
                    {
                        ID = reader.GetInt32(0),
                        TypeName = reader.GetString(1).Trim(),
                        Read = reader.GetBoolean(2),
                        Create = reader.GetBoolean(3),
                        Edit = reader.GetBoolean(4),
                        EditAll = reader.GetBoolean(5),
                        Delete = reader.GetBoolean(6),
                        DeleteAll = reader.GetBoolean(7),
                        RezTable = reader.GetBoolean(8)
                    });
                }
                oleDbConnection.Close();
            }
            Console.WriteLine($"[D] SELECT UserType count = {utypes.Count}");
            return utypes;
        }

        // GET: UserType/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserType> GetById(int id)
        {
            UserType utype;
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(string.Format("SELECT * FROM UserTypes WHERE ID = {0}", id), oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                if (reader.Read())
                {
                    utype = new UserType()
                    {
                        ID = reader.GetInt32(0),
                        TypeName = reader.GetString(1).Trim(),
                        Read = reader.GetBoolean(2),
                        Create = reader.GetBoolean(3),
                        Edit = reader.GetBoolean(4),
                        EditAll = reader.GetBoolean(5),
                        Delete = reader.GetBoolean(6),
                        DeleteAll = reader.GetBoolean(7),
                        RezTable = reader.GetBoolean(8)
                    };
                    oleDbConnection.Close();
                    return utype;
                }
            }
            return NotFound();
        }

        // POST: UserType/Insert
        [HttpPost]
        public ActionResult Insert([FromBody] UserType utype)
        {
            if (utype == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                    "INSERT INTO UserTypes ( TypeName, Read, Create, Edit, EditAll, Delete, DeleteAll, RezTable ) " +
                   $"VALUES ( '{utype.TypeName}', {utype.Read}, {utype.Create}, {utype.Edit}, {utype.EditAll}, {utype.Delete}, {utype.DeleteAll}, {utype.RezTable})",
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("INSERT INTO UserTypes ( TypeName, Read, Create, Edit, EditAll, Delete, DeleteAll, RezTable ) " +
                                     $"VALUES ( '{utype.TypeName}', {utype.Read}, {utype.Create}, {utype.Edit}, {utype.EditAll}, {utype.Delete}, {utype.DeleteAll}, {utype.RezTable})");
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // GET: UserType/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] UserType utype)
        {
            if (utype == null)
            {
                return BadRequest();
            }
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand(
                       "UPDATE Users " +
                      $"SET TypeName = '{utype.TypeName}', Read = {utype.Read}, Create = {utype.Create}, Edit = {utype.Edit}, EditAll = {utype.EditAll}, Delete = {utype.Delete}, DeleteAll = {utype.DeleteAll}, RezTable = {utype.RezTable} " +
                      $"WHERE (ID = {id}) ",
                    oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(string.Format("[D] UPDATE UserTypes ID = {0}", id));
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        // GET: UserType/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(GameShopContext.connectionString))
            {
                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand($"DELETE * FROM UserTypes WHERE ID = {id}", oleDbConnection);
                if (oleDbCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine($"DELETE * FROM UserTypes WHERE ID = {id}");
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
    }
}
