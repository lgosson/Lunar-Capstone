﻿using System;
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
          
            var serviceAdded = service;
            IEnumerable<Service> enumerable = serviceRepository.GetServices();
            List<Service> servs = enumerable.ToList();

            foreach (var postItem in servList)
            {
                foreach (var serv in servs)
                {
                    int p = Convert.ToInt32(postItem);
                    var itemToAdd = serviceRepository.GetServiceByID(p);
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
                serviceRepository.InsertService(new Service
                {
                    ID = service.ID,
                    Name = service.Name,
                    Description = service.Description,
                    Selectable = service.Selectable,
                    ConnectedServices = service.ConnectedServices,
                    ImageURL = service.ImageURL
                });
                serviceRepository.Save();
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
            int id2 = id.GetValueOrDefault();
            Service service = serviceRepository.GetServiceByID(id2);
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
            foreach (var postItem in servList)
            {
                foreach (var serv in servs)
                {
                    int p = Convert.ToInt32(postItem);
                    var itemToAdd = serviceRepository.GetServiceByID(p);
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
