using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WebAPI_Core_Magerramov.Models
{

    public class Game
    {
        public int ID { get; set; }
        public string NameG { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short Rating { get; set; }

    }
}
