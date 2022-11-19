using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi.Models
{
    public class ExtUser : User
    {
        public new UserType UserType { set; get; }
    }
}
