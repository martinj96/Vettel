namespace Transpo.Infrastructure.Data.Migrations
{
    using Entities;
    using Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Transpo.Infrastructure.Data.TranspoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Transpo.Infrastructure.Data.TranspoDbContext context)
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

            AutomaticMigrationsEnabled = true;

            context.Roles.AddOrUpdate(
                r => r.Name,
                new AppRole { Name = "Admin" });

            if (!(context.Users.Any(u => u.UserName == "admin@kinisaj.mk")))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new AppUserManager(userStore);
                var userToInsert = new AppUser
                {
                    UserName = "admin@kinisaj.mk",
                    Email = "admin@kinisaj.mk",
                    User = new User
                    {
                        Name = "Admin",
                        Email = "admin@kinisaj.mk"
                    }
                };
                userManager.Create(userToInsert, "Saksija123");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }
        }
    }
}
