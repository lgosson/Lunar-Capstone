using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
{
    public class ServiceRepository : IServiceRepository, IDisposable
    {
        private ServiceContext context;

        public ServiceRepository(ServiceContext context)
        {
            this.context = context;
        }

        public IEnumerable<Service> GetServices()
        {
            return context.Services.ToList();
        }

        public Service GetServiceByID(int ID)
        {
            Service service = context.Services.Find(ID);
            return service;
        }

        public void InsertService(Service service)
        {
            context.Services.Add(service);
        }

        public void DeleteService(int ID)
        {
            foreach(Service s in context.Services)
            {
                foreach (Service con in s.ConnectedServices)
                {
                    if (con.ID == ID)
                    {
                        s.ConnectedServices.Remove(con);
                    }

                }
            }

            Service service = context.Services.Find(ID);
            context.Services.Remove(service);
        }

        public void UpdateService(Service service)
        {
            context.Entry(service).State = EntityState.Modified;
        }

        public void Save()
        {
           // context.Entry(account).State = EntityState.Modified;
            context.SaveChanges();
        }

        private bool disposed = false;

        

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}