using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi.Models
{
    public class Basket
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int Count { get; set; }
        public int UserID { get; set; }

    }
}
