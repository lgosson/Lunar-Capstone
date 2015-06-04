using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LunarLogic.DAL;
using LunarLogic.Models;

namespace LunarLogic.Controllers
{
    public class ManageServicesController : Controller
    {
        //private ServiceContext db = new ServiceContext();

        private IServiceRepository serviceRepository;

        public ManageServicesController()
        {
            this.serviceRepository = new ServiceRepository(new ServiceContext());
        }

        public ManageServicesController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        // GET: ManageServices
        public ActionResult Index()
        {
            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servs = enumerable.ToList();

            return View(servs);
        }

        // GET: ManageServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id2 = id.GetValueOrDefault();
            Service service = serviceRepository.GetServiceByID(id2);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: ManageServices/Create
        public ActionResult Create()
        {
            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servs = enumerable.ToList();

            ViewBag.ServiceList = new MultiSelectList(servs, "ID", "Name");

            return View();
        }

        // POST: ManageServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Service service, int[] servList)
        {
            if (!ModelState.IsValid || servList == null || servList.Length <= 0) return View(service);

            var serviceAdded = service;
            serviceAdded.ConnectedServices = new List<Service>();
            List<Service> servs = serviceRepository.GetServices().ToList();

            foreach (var postItem in servList)
            {
                int p = Convert.ToInt32(postItem);
                foreach (var serv in servs)
                {
                    if (p == serv.ID)
                    {
                        if (!serviceAdded.ConnectedServices.Contains(serv))
                        {
                            serviceAdded.ConnectedServices.Add(serv);
                        }
                        //add this service to the service's connected service list that it is currently adding to it's own. 
                        //otherwise the newly added service will be the only one that knows what it is connected to.
                        serv.ConnectedServices.Add(serviceAdded);
                    }
                }
            }

            //db.Entry(serviceAdded).State = EntityState.Added;
            serviceRepository.InsertService(serviceAdded);
            serviceRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: ManageServices/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id2 = id.GetValueOrDefault();
            Service service = serviceRepository.GetServiceByID(id2);
            if (service == null)
            {
                return HttpNotFound();
            }


            var conList = service.ConnectedServices.ToList();
            List<int> intList = new List<int>();
            ViewBag.ConList = conList;
            foreach (var item in conList)
            {
                int i = item.ID;
                intList.Add(i);
            }

            List<string> disabledList = new List<string>();
            disabledList.Add(service.ID.ToString());

            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servs = enumerable.ToList();

            ViewBag.ServiceList = new MultiSelectList(servs, "ID", "Name", intList.ToArray(), disabledList);




            return View(service);
        }

        // POST: ManageServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit(Service service, int[] servList)
        {
            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servs = enumerable.ToList();

            var serviceChanged = (from s in serviceRepository.GetServices()
                                  where s.ID == service.ID
                                  select s).FirstOrDefault();

            if (servList.Length > 0) serviceChanged.ConnectedServices.Clear();

            foreach (var postItem in servList)
            {
                foreach (var serv in servs)
                {
                    int p = Convert.ToInt32(postItem);
                    var newConnection = serviceRepository.GetServiceByID(p);
                    if (p == serv.ID)
                    {
                        if (serviceChanged.ConnectedServices != null)
                        {
                            if (newConnection != null)
                            {
                                if (!serviceChanged.ConnectedServices.Contains(newConnection)) serviceChanged.ConnectedServices.Add(newConnection);
                                //the changed service added a connection. The object it connected to needs to know that it has a connection to service changed.
                                if (!newConnection.ConnectedServices.Contains(serviceChanged)) newConnection.ConnectedServices.Add(serviceChanged);
                            }
                        }
                    }
                }
            }

            if (service.ImageURL != null)
                serviceChanged.ImageURL = service.ImageURL;

            if (ModelState.IsValid)
            {
                //db.Entry(serviceChanged).State = EntityState.Modified;
                serviceRepository.UpdateService(serviceChanged);

                serviceRepository.Save();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: ManageServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id2 = id.GetValueOrDefault();
            Service service = serviceRepository.GetServiceByID(id2);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: ManageServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Service service = serviceRepository.GetServiceByID(id);
            //remove all references to such a service
            var serviceToDelete = serviceRepository.GetServiceByID(id);
            foreach (var s in serviceRepository.GetServices())
            {
                if (s.ConnectedServices.Contains(serviceToDelete)) s.ConnectedServices.Remove(serviceToDelete);
            }
            serviceRepository.DeleteService(id);
            serviceRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                serviceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
