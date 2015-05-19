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
            /*
            var selectItems = from service in db.Services.ToList()
                              select new SelectListItem
                              {
                                  Text = service.Name,
                                  Value = service.ID.ToString()
                              }; */

            ViewBag.ServiceList = new MultiSelectList(db.Services.ToList(), "ID", "Name");

            return View();
        }

        // POST: ManageServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,ParentInclude,Selectable")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
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

            //ViewBag.ConnectedListString = (service.ConnectedServices.ToList()).ToString();
            //ViewBag.ConnectedList = service.ConnectedServices.ToList();
            
            var conList = service.ConnectedServices.ToList();
            List<int> intList = new List<int>();
            ViewBag.ConList = conList;
            foreach(var item in conList)
            {
                int i = item.ID;
                intList.Add(i);
            }
                       
            ViewBag.ServiceList = new MultiSelectList(db.Services.ToList(), "ID", "Name", intList.ToArray(), service.ID.ToString());
            


            return View(service);
        }

        // POST: ManageServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public ActionResult Edit(Service service, int[] listDeal)//, List<string> listDeal)
        {
            var serviceChanged = (from s in db.Services
                              where s.ID == service.ID
                              select s).FirstOrDefault();
            foreach (var postItem in listDeal)
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
