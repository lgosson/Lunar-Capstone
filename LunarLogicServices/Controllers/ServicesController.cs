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
            Service service = new Service() { Name = "Service", Description = "Comes From Server" };

            //need to find out how to translate object from json for use with javascript - services.js
            //IEnumerable<Service> services = new List<Service>() { new Service(){Name = "Test Service", Description = "Description"} };
            //here we will retrieve all services and return them
            return Json(service, JsonRequestBehavior.AllowGet);
        }
    }
}