using LunarLogic.DAL;
using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Text;
using System.Threading.Tasks;


namespace LunarLogic.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        //private ServiceContext db = new ServiceContext();

        private IServiceRepository serviceRepository;

        public HomeController()
        {
            this.serviceRepository = new ServiceRepository(new ServiceContext());
        }

        public HomeController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

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
            #region pre-database test data
            //test. Eventually services come from db
            
            //Service s1 = new Service() { ID = 1, Selectable = false, Name = "Lunar Logic", Description = "Comes From Server. Has a really long description for reasons. It is called Service01 because it is the base service (which may not even BE a service)." };
           // Service s2 = new Service() { ID = 2, Name = "Service02", Description = "Another Service" };
            //Service s3 = new Service() { ID = 3, Name = "Service03", Description = "The Third Service" };
            //Service s4 = new Service() { ID = 4, Name = "Service04", Description = "One Service Too Many" };
            //Service s5 = new Service() { ID = 5, Name = "Service05", Description = "Another service" };
            //Service s6 = new Service() { ID = 6, Name = "Service06", Description = "Another again" };
           // Service s7 = new Service() { ID = 7, Name = "Service07", Description = "Yet another" };
            //Service s8 = new Service() { ID = 8, Name = "Service08", Description = "Yet another" };

            //s1.ConnectedServices = new List<Service>() { s2, s5, s6, s7, s8};
            //s2.ConnectedServices = new List<Service>() { s1, s3, s4 };
            //s3.ConnectedServices = new List<Service>() { s2 };
           // s4.ConnectedServices = new List<Service>() { s2 };
            //s5.ConnectedServices = new List<Service>() { s1 };
            //s6.ConnectedServices = new List<Service>() { s1 };
            //s7.ConnectedServices = new List<Service>() { s1 };
           // s8.ConnectedServices = new List<Service>() { s1 };

            //s1.ParentServices = new List<Service>();
            //s2.ParentServices = s2.ConnectedServices;
            //s3.ParentServices = s3.ConnectedServices;
           // s4.ParentServices = s4.ConnectedServices;
           // s5.ParentServices = s5.ConnectedServices;
           // s6.ParentServices = s6.ConnectedServices;
           // s7.ParentServices = s7.ConnectedServices;
           // s8.ParentServices = s8.ConnectedServices;
            //new List<Service>(){s1,s2,s3,s4,s5,s6,s7,s8};
            #endregion data

            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servicesToConvert = enumerable.ToList();

           

           List<ServiceViewModel> svms  = new List<ServiceViewModel>(){};
            foreach(Service s in servicesToConvert)
            {
                svms.Add(new ServiceViewModel(s));
            }

            return Json(svms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var email = "lunarlogicservices@gmail.com";
                    var username = "azure_3659b33334761d76b7482ecdfe51fea0@azure.com";
                    var password = "0AomnSyKtNRY23d";

                    StringBuilder sb = new StringBuilder();

                    sb.Append("First name: " + c.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Phone: " + c.Phone);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.Comment);

                    var msg = new SendGridMessage();

                    msg.From = new MailAddress(email, "Lunar Logic Services Website");
                    msg.AddTo(email);
                    msg.Subject = "Interested in Lunar Logic's Services";
                    msg.Text = sb.ToString();

                    //hardcoded send grid account cred (Azure)
                    var cred = new NetworkCredential(username, password);
                    var transportWeb = new Web(cred);

                    await transportWeb.DeliverAsync(msg);


                    //display sent msg
                }
                catch
                {
                    //display fail message
                    return View("Index");
                }
            }
            return View();
        }
    }
}