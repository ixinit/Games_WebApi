using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games_WebApi.Rezerv;

namespace Games_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervController : Controller
    {
        RezervDbManager rezerv;
        public RezervController()
        {
            rezerv = new RezervDbManager();
        }

        [HttpGet]
        public List<string> Index()
        {
            rezerv = new RezervDbManager();
            Console.WriteLine("Get files path");
            return rezerv.DataBases;
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            rezerv.createCopy();
            Console.WriteLine("Create db copy.");
            return new OkResult();
        }

        [HttpGet]
        [Route("Curent")]
        public string Curent()
        {
            Console.WriteLine("Get curent db file");
            return rezerv.CurentDbFile;
        }

        [HttpGet]
        [Route("Curent/{id}")]
        public ActionResult setDb(int id)
        {
            rezerv.setNewDbFile(id);
            return new OkResult();
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            rezerv.deleteCopy(id);
            Console.WriteLine($"Delete db copy {id}");
            return new OkResult();
        }

    }
}
