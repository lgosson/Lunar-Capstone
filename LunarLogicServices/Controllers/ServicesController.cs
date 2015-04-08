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
            Service s1 = new Service(){ID = 1, Name = "Service01", Description = "Comes From Server"};
            Service s2 = new Service(){ID = 2, Name = "Service02", Description = "Another Service"};
            Service s3 = new Service(){ID = 3, Name = "Service03", Description = "The Third Service"};
            Service s4 = new Service(){ID = 4, Name = "Service04", Description = "Once Service Too Many"};

            s1.ConnectedServices = new List<Service>() { s2 };
            s2.ConnectedServices = new List<Service>() { s3 };
            s3.ConnectedServices = new List<Service>() { s4 };


            //here we will retrieve all services and return them
            IEnumerable<Service> services  = new List<Service>(){ s1,s2,s3,s4 };
            return Json(services, JsonRequestBehavior.AllowGet);
        }
    }
}