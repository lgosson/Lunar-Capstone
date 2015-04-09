using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarLogicServices.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ConnectedServices { get; set; }
    }
}