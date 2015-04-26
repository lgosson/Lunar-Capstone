using LunarLogicServices.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LunarLogicServices.DAL
{
    public class ServiceContext : DbContext
    {
        public ServiceContext() : base("ServiceContext")
        {

        }
    

    
    public DbSet<Service> Services { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
