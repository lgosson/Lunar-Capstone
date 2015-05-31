using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
{
    public interface IServiceRepository : IDisposable
    {
        IEnumerable<Service> GetServices();
        Service GetServiceByID(int id);
        void InsertService(Service service);
        void DeleteService(int id);
        void UpdateService(Service service);
        void Save();
    }
}