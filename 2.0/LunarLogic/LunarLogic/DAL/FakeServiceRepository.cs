using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
{
    public class FakeServiceRepository : IServiceRepository
    {
        List<Service> services;

        public FakeServiceRepository()
        {
            services = new List<Service>();
        }

        public FakeServiceRepository(List<Service> svcs)
        {
            services = svcs;
        }

        public IEnumerable<Models.Service> GetServices()
        {
            return services;
        }

        public Models.Service GetServiceByID(int id)
        {
            return (from s in services
                    where s.ID == (int)id
                    select s).FirstOrDefault();
        }

        public void InsertService(Models.Service service)
        {
            services.Add(service);
        }

        public virtual void DeleteService(int id)
        {
            Service serviceToDelete = GetServiceByID(id);
            DeleteService(serviceToDelete);
        }

        public virtual void DeleteService(Service serviceToDelete)
        {
            services.Remove(serviceToDelete);
        }

        public virtual void UpdateService(Models.Service service)
        {
            // simulate state?
        }

        public void Save()
        {
            // probably won't need
        }

        public void Dispose()
        {
            // probably won't need
            throw new NotImplementedException();
        }
    }
}