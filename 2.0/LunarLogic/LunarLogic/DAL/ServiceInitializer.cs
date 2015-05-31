using LunarLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarLogic.DAL
{
    public class ServiceInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges <ServiceContext>
        //DropCreateDatabaseAlways<ServiceContext>
    {
        protected override void Seed(ServiceContext context)
        {
            Service s1 = new Service() { ID = 1, Selectable = false, Name = "Lunar Logic", ImageURL = "/images/serviceicon1.png", Description = "Comes From Server.." };
            Service s2 = new Service()
            {
                ID = 2,
                Name = "Service02",
                ImageURL = "/images/serviceicon1.png",
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a"  };
            Service s3 = new Service() { ID = 3, Name = "Service03", ImageURL = "/images/serviceicon1.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s4 = new Service() { ID = 4, Name = "Service04", ImageURL = "/images/serviceicon2.png", Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui" };
            Service s5 = new Service() { ID = 5, Name = "Service05", ImageURL = "/images/serviceicon3.png", Description = "Li Europan lingues es membres del sam familie. Lor separat existentie es un myth. Por scientie, musica, sport etc, litot Europa usa li sam vocabular. Li lingues differe solmen in li grammatica, li pronunciation e li plu commun vocabules. Omnicos directe al desirabilite de un nov lingua franca: On refusa continuar payar custosi traductores. At solmen va esser necessi far uniform grammatica, pronunc" };
            Service s6 = new Service() { ID = 6, Name = "Service06", ImageURL = "/images/serviceicon4.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s7 = new Service() { ID = 7, Name = "Service07", ImageURL = "/images/serviceicon5.png", Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui" };
            Service s8 = new Service() { ID = 8, Name = "Service08", ImageURL = "/images/serviceicon6.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s9 = new Service() { ID = 9, Name = "Service09", ImageURL = "/images/serviceicon6.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s10 = new Service() { ID = 10, Name = "Service10", ImageURL = "/images/serviceicon7.png", Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui" };
            Service s11 = new Service() { ID = 11, Name = "Service11", ImageURL = "/images/serviceicon8.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s12 = new Service() { ID = 12, Name = "Service12", ImageURL = "/images/serviceicon2.png", Description = "Li Europan lingues es membres del sam familie. Lor separat existentie es un myth. Por scientie, musica, sport etc, litot Europa usa li sam vocabular. Li lingues differe solmen in li grammatica, li pronunciation e li plu commun vocabules. Omnicos directe al desirabilite de un nov lingua franca: On refusa continuar payar custosi traductores. At solmen va esser necessi far uniform grammatica, pronunc" };
            Service s13 = new Service() { ID = 13, Name = "Service13", ImageURL = "/images/serviceicon4.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };
            Service s14 = new Service() { ID = 14, Name = "Service14", ImageURL = "/images/serviceicon3.png", Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a" };

            s1.ConnectedServices = new List<Service>() { s2, s5, s6, s7, s8 };
            s2.ConnectedServices = new List<Service>() { s1, s3, s4 };
            s3.ConnectedServices = new List<Service>() { s2 };
            s4.ConnectedServices = new List<Service>() { s2 };
            s5.ConnectedServices = new List<Service>() { s1 };
            s6.ConnectedServices = new List<Service>() { s1 };
            s7.ConnectedServices = new List<Service>() { s1, s14 };
            s8.ConnectedServices = new List<Service>() { s1, s10 };
            s9.ConnectedServices = new List<Service>() { s3, s11 };
            s10.ConnectedServices = new List<Service>() { s8, s12, s14 };
            s11.ConnectedServices = new List<Service>() { s9 };
            s12.ConnectedServices = new List<Service>() { s10 };
            s13.ConnectedServices = new List<Service>() { s6 };
            s14.ConnectedServices = new List<Service>() { s10, s7 };

            s1.ParentServices = new List<Service>() { s1 };
            s2.ParentServices = new List<Service>() { s1, s3, s4 };
            s3.ParentServices = new List<Service>() { s2 };
            s4.ParentServices = new List<Service>() { s2 };
            s5.ParentServices = s5.ConnectedServices;
            s6.ParentServices = s6.ConnectedServices;
            s7.ParentServices = new List<Service>() { s1 };
            s8.ParentServices = s8.ConnectedServices;
            s9.ParentServices = new List<Service>() { s3 };
            s10.ParentServices = new List<Service>() { s8 };
            s11.ParentServices = s11.ConnectedServices;
            s12.ParentServices = s12.ConnectedServices;
            s13.ParentServices = s13.ConnectedServices;
            s14.ParentServices = new List<Service>() { s10 };
            
            var services = new List<Service> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14 };

            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            


        }
    }
}