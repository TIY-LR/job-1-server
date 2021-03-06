namespace JobTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JobTracker.Models.ApplicationDbContext context)
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
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (!(context.Users.Any(u => u.UserName == "joe@nobody.com")))
            {
                var userToInsert = new ApplicationUser { UserName = "joe@nobody.com" };
                userManager.Create(userToInsert, "Abc123!@#");
            }
            context.Contacts.AddOrUpdate(
                x => x.Email,
                new Contact { FirstName = "Adam", LastName = "Ant", Address1 = "1313 Mockingbird Lane", CellNumber = "501-867-5309", Email = "adamant@acme.com" },
                new Contact { FirstName = "Betty", LastName = "Boop", Address1 = "2626 Jaybird", CellNumber = "501-555-2629", Email = "bettyboop@love.org" }
                 );
           
           
            context.Orgs.AddOrUpdate(
                x => x.Name,
                new Org { Name = "Acme Explosives", Address1 = "2020 Explosive Lane", City = "Boomtown", State = "AZ", Website = "http://www.acmeblows.com" },
                 new Org { Name = "Peace and Love Charity", Address1 = "69 Olive Branch Way", City = "Angeles", State = "CA", Website = "http://weloveeveryone.com" }
                );
            

        }
    }
}
