using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LunarLogic.Models
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

        public virtual ICollection<Service> ConnectedServices { get; set; }
        public virtual ICollection<Service> ParentServices { get; set; }
        //parent services are redundant entries for connected services, but are used just to track which connections are parent/child relationships
    }

    /// <summary>
    /// This view model exists to translate a service into a format acceptable for the client.
    /// </summary>
    public class ServiceViewModel
    {
        public ServiceViewModel(Service s)
        {
            ID = s.ID.ToString();
            Name = s.Name;
            Description = s.Description;
            Selectable = s.Selectable;
            ParentInclude = s.ParentInclude;

            ConnectedServices = new List<string>();
            foreach (Service connected in s.ConnectedServices)
            {
                ConnectedServices.Add(connected.ID.ToString());
            }

            //only one parent service can currently exist. (This is a workaround for not being able to change the nav prop ParentServices for the Service model)
            foreach (Service serv in s.ParentServices) ParentService = serv.ID.ToString();
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ParentInclude { get; set; }
        public bool Selectable { get; set; }
        public List<string> ConnectedServices { get; set; }
        public string ParentService { get; set; }
    }
}