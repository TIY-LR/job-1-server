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
            AutomaticMigrationDataLossAllowed = true;
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

            context.Orgs.AddOrUpdate(
               x => x.Name,
               new Org { Id = 1, Name = "Acme Explosives", Address1 = "2020 Explosive Lane", City = "Boomtown", State = "AZ", Website = "http://www.acmeblows.com" },
                new Org { Id = 2, Name = "Peace and Love Charity", Address1 = "69 Olive Branch Way", City = "Angeles", State = "CA", Website = "http://weloveeveryone.com" }
               );

            context.Contacts.AddOrUpdate(
                x => x.Email,
                new Contact { Id = 1, FirstName = "Adam", LastName = "Ant", Address1 = "1313 Mockingbird Lane", CellNumber = "501-867-5309", Email = "adamant@acme.com", Org_Id = 1 },
                new Contact { Id = 2, FirstName = "Betty", LastName = "Boop", Address1 = "2626 Jaybird", CellNumber = "501-555-2629", Email = "bettyboop@love.org", Org_Id = 2 }
                 );

            context.Positions.AddOrUpdate(
                x => x.Title,
                new Position { Id = 1, Title = "Explosives Engineer", Contact_Id = 1, Org_Id = 1, Education = "Bachelor's in Engineering from Accredited University", Experience = "3 - 5 years", Description = "Do you love blowing stuff up?  Then this job is for you!", SalaryMax = 100000, SalaryMin = 60000 },
                new Position { Id = 2, Title = "Overseas Charity Worker", Contact_Id = 2, Org_Id = 2, Education = "HS Diploma, Bachelor's degree preferred", Description = "If you love helping people, come join our team!", Experience = "none required but prefer community volunteer experience", SalaryMin = 100, SalaryMax = 20000 }
                );

            context.Events.AddOrUpdate(
                x => x.Title,
                new Event { Title = "Interview at Acme", Date = DateTime.Now, Time = DateTime.Now, Org_Id = 1, Contact_Id = 1, Position_Id = 1 },
                new Event { Title = "Site Visit at PeaceLove", Date = DateTime.Now, Time = DateTime.Now, Org_Id = 2, Contact_Id = 2, Position_Id = 2 }
                );

        }
    }
}
