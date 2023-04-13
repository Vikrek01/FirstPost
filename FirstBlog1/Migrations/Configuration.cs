namespace FirstBlog1.Migrations
{
    using FirstBlog1.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirstBlog1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstBlog1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.Add(new User { UserName = "Admin", Password = "admin", Role = "Admin" });
            context.Users.Add(new User { UserName = "Aman", Password = "aman", Role = "User" });



            context.SaveChanges();
            base.Seed(context);
        }
    }
}
