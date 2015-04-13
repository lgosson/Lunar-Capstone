using LunarLogicServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunarLogicServices.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return RedirectToAction("Services");
        }

        public ActionResult Services()
        {
            return View();
        }

        
        public JsonResult GetServiceData()
        {
            //test. Eventually services come from db
            Service s1 = new Service() { ID = 1, Name = "Service01", Description = "Comes From Server" };
            Service s2 = new Service() { ID = 2, Name = "Service02", Description = "Another Service" };
            Service s3 = new Service() { ID = 3, Name = "Service03", Description = "The Third Service" };
            Service s4 = new Service() { ID = 4, Name = "Service04", Description = "One Service Too Many" };
            Service s5 = new Service() { ID = 5, Name = "Service05", Description = "Another service" };
            Service s6 = new Service() { ID = 6, Name = "Service06", Description = "Another again" };
            Service s7 = new Service() { ID = 7, Name = "Service07", Description = "Yet another" };
            Service s8 = new Service() { ID = 8, Name = "Service08", Description = "Yet another" };

            s1.ConnectedServices = new List<string>() { s2.Name, s5.Name, s6.Name, s7.Name, s8.Name};
            s2.ConnectedServices = new List<string>() { s1.Name, s3.Name, s4.Name };
            s3.ConnectedServices = new List<string>() { s2.Name };
            s4.ConnectedServices = new List<string>() { s2.Name };
            s5.ConnectedServices = new List<string>() { s1.Name };
            s6.ConnectedServices = new List<string>() { s1.Name };
            s7.ConnectedServices = new List<string>() { s1.Name };
            s8.ConnectedServices = new List<string>() { s1.Name };

            s2.ParentServices = s2.ConnectedServices;
            s3.ParentServices = s3.ConnectedServices;
            s4.ParentServices = s4.ConnectedServices;
            s5.ParentServices = s5.ConnectedServices;
            s6.ParentServices = s6.ConnectedServices;
            s7.ParentServices = s7.ConnectedServices;
            s8.ParentServices = s8.ConnectedServices;


            //here we will retrieve all services and return them
            IEnumerable<Service> services  = new List<Service>(){s1,s2,s3,s4,s5,s6,s7, s8};
            return Json(services, JsonRequestBehavior.AllowGet);
        }
    }
}