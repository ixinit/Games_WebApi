using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI_Core_Magerramov.Models;
using System.Data.OleDb;

namespace WebAPI_Core_Magerramov.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GameController : Controller
    {
        //String connectionString = @"Data Source=com-234-2\sqlexpress;Initial Catalog=DB_Magerramov;Integrated Security=True";
        //public static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Workers.mdb;";
        public static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\bin\x86\db1.mdb";
        [HttpGet]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
        public List<Game> Get()
        {
            List<Game> cat = new List<Game>();
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {

                oleDbConnection.Open();
                OleDbCommand oleDbCommand = new OleDbCommand("Select * from Games", oleDbConnection);
                OleDbDataReader reader = oleDbCommand.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetInt32(0));
                    cat.Add(new Game()
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
            return cat;


        }

        // GET: GameController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GameController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GameController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
