using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi.Models
{
    public class ExtBasket
    {
        public int ID { get; set; }
        public string NameG { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
