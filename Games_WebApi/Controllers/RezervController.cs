using Games_WebApi.Rezerv;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Games_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervController : Controller
    {

        [HttpGet]
        public List<string> Index()
        {
            Console.WriteLine("Get files path");
            return RezervDbManager.DataBases;
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            RezervDbManager.createCopy();
            Console.WriteLine("Create db copy.");
            return new OkResult();
        }

        [HttpGet]
        [Route("Curent")]
        public string Curent()
        {
            Console.WriteLine("Get curent db file");
            return RezervDbManager.CurentDbFile;
        }

        [HttpGet]
        [Route("Curent/{id}")]
        public ActionResult setDb(int id)
        {
            RezervDbManager.setNewDbFile(id);
            return new OkResult();
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            RezervDbManager.deleteCopy(id);
            Console.WriteLine($"Delete db copy {id}");
            return new OkResult();
        }

        [HttpGet]
        [Route("MaxRezervs/{id}")]
        public ActionResult setMaxRezervs(int id)
        {
            RezervDbManager.Settings.MaxRezervs = id;
            Console.WriteLine($"MaxRezervs now {id}");
            return new OkResult();
        }

        [HttpGet]
        [Route("TimerToCreate/{id}")]
        public ActionResult setTimerToCreate(int id)
        {
            RezervDbManager.Settings.TimerToCreate = id;
            Console.WriteLine($"TimerToCreate now {id}");
            return new OkResult();
        }
    }
}
