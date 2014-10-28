using System.Diagnostics.CodeAnalysis;
using AirManager.Infrastructure.Models;
using Xunit;

namespace AirManager.Infrastructure.Tests
{
    public class GameDataFixture
    {
        [Fact, ExcludeFromCodeCoverage]
        public void CanGetAndSetCountries()
        {
            var countries = new[] {new Country{Id = "CA", Name = "Canada"}, new Country {Id = "US", Name = "United States"}};

            var data = new GameData();

            data.Countries = countries;

            Assert.Equal(data.Countries, countries);
        }
    }
}