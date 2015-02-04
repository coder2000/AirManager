using System.Data.Entity.Migrations;
using System.Linq;
using AirManager.Infrastructure.Models;
using AirManager.Infrastructure.Seeder;

namespace AirManager.Infrastructure.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<AirManagerContext>
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

            const string countries = "AirManager.Infrastructure.SeedData.country.csv";
            context.Countries.SeedFromResource(countries, c => c.Id);

            context.SaveChanges();

            const string airports = "AirManager.Infrastructure.SeedData.airports-canada.csv";
            context.Airports.SeedFromResource(airports, a => a.Id, new CsvColumnMapping<Airport>("CountryId",
                (airport, countryId) => { airport.Country = context.Countries.Single(c => c.Id == (string) countryId); }));
        }
    }
}