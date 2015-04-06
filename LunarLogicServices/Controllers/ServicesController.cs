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
            IEnumerable<string> testData = new List<string>(){"test", "testAgain"};
            //here we will retrieve all services and return them
            return Json(testData, JsonRequestBehavior.AllowGet);
        }

        //Here is a useful link for how someone uses JSON and AJAX to transmit data between client js and server c#
        //http://codeforcoffee.org/asp-net-mvc-intro-to-mvc-using-binding-json-objects-to-models/
    }
}