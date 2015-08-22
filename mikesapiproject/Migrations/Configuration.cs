namespace mikesapiproject.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<mikesapiproject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(mikesapiproject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Posts.AddOrUpdate(x => x.Title,
                new Post() {Title="Joy of Barking", Author="Mr. Peabody", Body= "Lorem ipsum dolor sit amet, mea an quot mutat ancillae. Ius ut quod idque, exerci eruditi an quo. Ponderum indoctum est ei, in vix omnis saepe putent, at duo malis erant dolor. Virtute eloquentiam instructior nec et. Ne paulo prodesset temporibus usu, alia simul sit in." },
                new Post() { Title = "My Journey to Iceland", Author = "Chilly Coldwater", Body = "Ea pri philosophia consequuntur, pro et nonumes accommodare, in corpora copiosae usu. Et nam malis persequeris. His ut quas accusam omnesque, exerci assentior vix eu. Sed ea epicuri placerat, vim velit tincidunt te." },  
                new Post() { Title = "Time is Against Us", Author = "Waite A. Minnit", Body = "His no laoreet percipit, quo solet legere voluptatum id, in eos quidam docendi patrioque. Mea ea tota veritus civibus, sit te porro moderatius. Mei scripta officiis et. Te posse nobis denique usu, et vix epicuri adolescens, ad pri enim debitis. Tale cetero comprehensam id duo, habemus persequeris et sit, pri ut rebum cetero deterruisset. Summo doming accusata ei mei, dico solum graeco vim et." },
                new Post() { Title = "The Beginner's Guide To Collecting Old Coffee Mugs", Author = "Betty Runsoff", Body = "Vim ne delicata sententiae. Quas mundi omnes mel in. Ut quaeque ancillae accommodare duo, in has recusabo signiferumque. Alii minim consetetur mei cu." }
                );
        }
    }
}
