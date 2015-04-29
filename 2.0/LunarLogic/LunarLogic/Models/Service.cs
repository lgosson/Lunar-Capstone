using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LunarLogicServices.Models
{
    public class Service
    {
        public Service()
        {
            Selectable = true;
        }
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ParentInclude { get; set; }
        public bool Selectable { get; set; }
        public IEnumerable<string> ConnectedServices { get; set; }
        public IEnumerable<string> ParentServices { get; set; }
        //parent services are redundant entries for connected services, but are used just to track which connections are parent/child relationships
    }
}