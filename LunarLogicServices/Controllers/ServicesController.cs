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
            //here we will retrieve all services and return them
            IEnumerable<Service> services  = new List<Service>(){ 
                new Service(){ID = 1, Name = "Service01", Description = "Comes From Server"},
                new Service(){ID = 2, Name = "Service02", Description = "Another Service"},
                new Service(){ID = 3, Name = "Service03", Description = "The Third Service"},
                new Service(){ID = 4, Name = "Service04", Description = "Once Service Too Many"}
            };
            return Json(services, JsonRequestBehavior.AllowGet);
        }
    }
}