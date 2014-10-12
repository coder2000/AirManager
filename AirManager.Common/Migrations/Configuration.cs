using System.Data.Entity.Migrations;
using EntityFramework.Seeder;

namespace AirManager.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AirManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AirManagerContext context)
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

            const string csv = "AirManager.Infrastructure.SeedData.country.csv";
            context.Countries.SeedFromResource(csv, c => c.Id);
        }
    }
}