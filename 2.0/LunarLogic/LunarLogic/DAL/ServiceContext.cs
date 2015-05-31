using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
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
            //modelBuilder.Entity<Service>()
            //.HasOptional(a => a.ConnectedServices)
            //.WithOptionalDependent()
            //.WillCascadeOnDelete(true);
        }

    public System.Data.Entity.DbSet<LunarLogic.Models.ServiceListViewModel> ServiceListViewModels { get; set; }
    }
}
