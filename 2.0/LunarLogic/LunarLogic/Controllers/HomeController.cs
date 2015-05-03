using LunarLogicServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunarLogic.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
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

        public ActionResult ServiceInformation(ServiceViewModel s)
        {
            return PartialView("_ServiceInformation", s);
        }

        public ActionResult ContactForm(Contact c)
        {
            return PartialView("_ContactForm", c);
        }
        
        public JsonResult GetServiceData()
        {
            //test. Eventually services come from db
            Service s1 = new Service() { ID = 1, Selectable = false, Name = "Lunar Logic", Description = "Comes From Server. Has a really long description for reasons. It is called Service01 because it is the base service (which may not even BE a service)." };
            Service s2 = new Service() { ID = 2, Name = "Service02", Description = "Another Service" };
            Service s3 = new Service() { ID = 3, Name = "Service03", Description = "The Third Service" };
            Service s4 = new Service() { ID = 4, Name = "Service04", Description = "One Service Too Many" };
            Service s5 = new Service() { ID = 5, Name = "Service05", Description = "Another service" };
            Service s6 = new Service() { ID = 6, Name = "Service06", Description = "Another again" };
            Service s7 = new Service() { ID = 7, Name = "Service07", Description = "Yet another" };
            Service s8 = new Service() { ID = 8, Name = "Service08", Description = "Yet another" };

            s1.ConnectedServices = new List<Service>() { s2, s5, s6, s7, s8};
            s2.ConnectedServices = new List<Service>() { s1, s3, s4 };
            s3.ConnectedServices = new List<Service>() { s2 };
            s4.ConnectedServices = new List<Service>() { s2 };
            s5.ConnectedServices = new List<Service>() { s1 };
            s6.ConnectedServices = new List<Service>() { s1 };
            s7.ConnectedServices = new List<Service>() { s1 };
            s8.ConnectedServices = new List<Service>() { s1 };

            s1.ParentServices = new List<Service>();
            s2.ParentServices = s2.ConnectedServices;
            s3.ParentServices = s3.ConnectedServices;
            s4.ParentServices = s4.ConnectedServices;
            s5.ParentServices = s5.ConnectedServices;
            s6.ParentServices = s6.ConnectedServices;
            s7.ParentServices = s7.ConnectedServices;
            s8.ParentServices = s8.ConnectedServices;


            List<Service> servicesToConvert = new List<Service>(){s1,s2,s3,s4,s5,s6,s7,s8};
            List<ServiceViewModel> svms  = new List<ServiceViewModel>(){};
            foreach(Service s in servicesToConvert)
            {
                svms.Add(new ServiceViewModel(s));
            }

            return Json(svms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Contact(Contact c)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        MailMessage msg = new MailMessage();
            //        SmtpClient smtp = new SmtpClient();
            //        MailAddress from = new MailAddress(c.Email.ToString());
            //        StringBuilder sb = new StringBuilder();
            //        msg.To.Add("jeseswood21@gmail.com");
            //        msg.Subject = "Contact Form";
            //        msg.IsBodyHtml = false;
            //        smtp.Host = "aspmx.l.google.com";
            //        smtp.Port = 25;
            //        sb.Append("First name: " + c.FirstName);
            //        sb.Append(Environment.NewLine);
            //        sb.Append("Last name: " + c.LastName);
            //        sb.Append(Environment.NewLine);
            //        sb.Append("Phone: " + c.Phone);
            //        sb.Append(Environment.NewLine);
            //        sb.Append("Email: " + c.Email);
            //        sb.Append(Environment.NewLine);
            //        sb.Append("Comments: " + c.Comment);
            //        msg.Body = sb.ToString();
            //        smtp.Send(msg);
            //        msg.Dispose();
            //    }
            //    catch
            //    {
            //        return View("Index");
            //    }
            //}
            //return View();

            return Json(new { result = "something worked" });
        }
    }
}