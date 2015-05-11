using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
{
    public class ServiceInitializer : System.Data.Entity. //DropCreateDatabaseIfModelChanges <ServiceContext>
        DropCreateDatabaseAlways<ServiceContext>
    {
        protected override void Seed(ServiceContext context)
        {
            Service s1 = new Service() { ID = 1, Selectable = false, Name = "Lunar Logic", Description = "Comes From Server. Has a really long description for reasons. It is called Service01 because it is the base service (which may not even BE a service)." };
            Service s2 = new Service() { ID = 2, Name = "Service02", Description = "Another Service" };
            Service s3 = new Service() { ID = 3, Name = "Service03", Description = "The Third Service" };
            Service s4 = new Service() { ID = 4, Name = "Service04", Description = "One Service Too Many" };
            Service s5 = new Service() { ID = 5, Name = "Service05", Description = "Another service" };
            Service s6 = new Service() { ID = 6, Name = "Service06", Description = "Another again" };
            Service s7 = new Service() { ID = 7, Name = "Service07", Description = "Yet another" };
            Service s8 = new Service() { ID = 8, Name = "Service08", Description = "Yet another" };
            Service s9 = new Service() { ID = 9, Name = "Service09", Description = "Service the ninth" };
            Service s10 = new Service() { ID = 10, Name = "Service10", Description = "Service the tenth" };
            Service s11 = new Service() { ID = 11, Name = "Service11", Description = "Problem service" };
            Service s12 = new Service() { ID = 12, Name = "Service12", Description = "Problem service again" };
            Service s13 = new Service() { ID = 13, Name = "Service13", Description = "Unlucky service" };
            Service s14 = new Service() { ID = 14, Name = "Service14", Description = "Entangled service" };

            s1.ConnectedServices = new List<Service>() { s2, s5, s6, s7, s8 };
            s2.ConnectedServices = new List<Service>() { s1, s3, s4 };
            s3.ConnectedServices = new List<Service>() { s2 };
            s4.ConnectedServices = new List<Service>() { s2 };
            s5.ConnectedServices = new List<Service>() { s1 };
            s6.ConnectedServices = new List<Service>() { s1 };
            s7.ConnectedServices = new List<Service>() { s1, s14 };
            s8.ConnectedServices = new List<Service>() { s1 };
            s9.ConnectedServices = new List<Service>() { s3, s11 };
            s10.ConnectedServices = new List<Service>() { s8, s12, s14 };
            s11.ConnectedServices = new List<Service>() { s9 };
            s12.ConnectedServices = new List<Service>() { s10 };
            s13.ConnectedServices = new List<Service>() { s6 };
            s14.ConnectedServices = new List<Service>() { s10, s7 };

            s1.ParentServices = s1.ConnectedServices;
            s2.ParentServices = s2.ConnectedServices;
            s3.ParentServices = s3.ConnectedServices;
            s4.ParentServices = s4.ConnectedServices;
            s5.ParentServices = s5.ConnectedServices;
            s6.ParentServices = s6.ConnectedServices;
            s7.ParentServices = s7.ConnectedServices;
            s8.ParentServices = s8.ConnectedServices;
            s9.ParentServices = s9.ConnectedServices;
            s10.ParentServices = s10.ConnectedServices;
            s11.ParentServices = s11.ConnectedServices;
            s12.ParentServices = s12.ConnectedServices;
            s13.ParentServices = s13.ConnectedServices;
            s14.ParentServices = s14.ConnectedServices;

            var services = new List<Service> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14 };

            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            


        }
    }
}