using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarLogic;
using LunarLogic.Controllers;
using LunarLogic.DAL;
using LunarLogic.Models;
using System.Web.Mvc;

namespace LunarLogic.Tests
{
    [TestClass]
    public class ManageServicesCrud_Tests
    {
        const string SERVICE1 = "Service1";

        [TestMethod]
        public void ManageServicesCrud_Index_Test()
        {
            // arrange
            List<Service> services = new List<Service>();
            services.Add(new Service {
                Name = SERVICE1
            });

            FakeServiceRepository fake = new FakeServiceRepository(services);
            ManageServicesController target = new ManageServicesController(fake);
            
            // act
            var view = (ViewResult)target.Index();

            // assert
            var model = (List<Service>)view.Model;
            Assert.AreEqual(SERVICE1, services[0].Name);
        }

        [TestMethod]
        public void ManageServicesCrud_InsertService_Test()
        {
            // arrange
            List<Service> services = new List<Service>();
            Service svc = new Service {Name=SERVICE1};
            int[] connectedSvcs = new int[] { 1, 2, 3 };

            FakeServiceRepository fake = new FakeServiceRepository(services);
            ManageServicesController target = new ManageServicesController(fake);

            // act 
            target.Create(svc, connectedSvcs);

            // assert
            Assert.AreEqual(SERVICE1, services[0].Name);
        }
    }
}
