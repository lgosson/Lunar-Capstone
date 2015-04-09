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

            s1.ConnectedServices = new List<string>() { s2.Name, s5.Name, s6.Name };
            s2.ConnectedServices = new List<string>() { s3.Name };
            s3.ConnectedServices = new List<string>() { s4.Name };
            s4.ConnectedServices = new List<string>() { s1.Name };
            s5.ConnectedServices = new List<string>() { s6.Name };
            s6.ConnectedServices = new List<string>() { s7.Name };
            s7.ConnectedServices = new List<string>() { s6.Name };


            //here we will retrieve all services and return them
            IEnumerable<Service> services  = new List<Service>(){s1,s2,s3,s4,s5,s6,s7};
            return Json(services, JsonRequestBehavior.AllowGet);
        }
    }
}