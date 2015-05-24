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
    [AllowAnonymous]
    public class ManageServicesController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: ManageServices
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: ManageServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: ManageServices/Create
        public ActionResult Create()
        {
            

            ViewBag.ServiceList = new MultiSelectList(db.Services.ToList(), "ID", "Name");

            return View();
        }

        // POST: ManageServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Service service, int[] servList)
        {
          
            var serviceAdded = service;

            foreach (var postItem in servList)
            {
                foreach (var serv in db.Services.ToList())
                {
                    int p = Convert.ToInt32(postItem);
                    var itemToAdd = db.Services.Find(p);
                    if (p == serv.ID)
                    {
                        serviceAdded.ConnectedServices = new List<Service>();
                        if (serviceAdded.ConnectedServices != null)
                        {
                            if (itemToAdd != null)
                            {
                                if (!serviceAdded.ConnectedServices.Contains(itemToAdd))
                                {
                                    serviceAdded.ConnectedServices.Add(itemToAdd);
                                }
                            }
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                //db.Entry(serviceAdded).State = EntityState.Added;
                db.Services.Add(new Service
                {
                    ID = service.ID,
                    Name = service.Name,
                    Description = service.Description,
                    Selectable = service.Selectable,
                    ConnectedServices = service.ConnectedServices
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: ManageServices/Edit/5
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

                        
            var conList = service.ConnectedServices.ToList();
            List<int> intList = new List<int>();
            ViewBag.ConList = conList;
            foreach(var item in conList)
            {
                int i = item.ID;
                intList.Add(i);
            }

            List<string> disabledList = new List<string>();
            disabledList.Add(service.ID.ToString());
            
            ViewBag.ServiceList = new MultiSelectList(db.Services.ToList(), "ID", "Name", intList.ToArray(), disabledList); 
                                                                             
            


            return View(service);
        }

        // POST: ManageServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit(Service service, int[] servList)
        {
            var serviceChanged = (from s in db.Services
                              where s.ID == service.ID
                              select s).FirstOrDefault();
            foreach (var postItem in servList)
            {
                foreach (var serv in db.Services.ToList())
                {
                    int p = Convert.ToInt32(postItem);
                    var itemToAdd = db.Services.Find(p);
                    if (p == serv.ID)
                    {

                        if (serviceChanged.ConnectedServices != null)
                        {
                            if (itemToAdd != null)
                            {
                                if (!serviceChanged.ConnectedServices.Contains(itemToAdd))
                                {
                                    serviceChanged.ConnectedServices.Add(itemToAdd);
                                }
                            }
                        }
                    }
                }
            }      
     
            
   
            if (ModelState.IsValid)
            {
                db.Entry(serviceChanged).State = EntityState.Modified;
                
                db.SaveChanges();
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
            Service service = db.Services.Find(id);
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
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
