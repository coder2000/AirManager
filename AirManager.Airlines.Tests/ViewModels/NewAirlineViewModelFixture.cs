using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using AirManager.Airlines.ViewModels;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Models;
using Moq;
using Xunit;

namespace AirManager.Airlines.Tests.ViewModels
{
    public class NewAirlineViewModelFixture
    {
        [Fact, ExcludeFromCodeCoverage]
        public void CanGetAndSetCountry()
        {
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();
            var players = new Mock<DbSet<Player>>();

            context.Setup(c => c.Players).Returns(players.Object);

            var viewModel = new NewAirlineViewModel(context.Object, data.Object);

            var country = new Country {Id = "CA", Name = "Canada"};

            viewModel.Country = country;

            Assert.Equal(viewModel.Country, country);
        }

        [Fact, ExcludeFromCodeCoverage]
        public void CanGetAndSetName()
        {
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();
            var players = new Mock<DbSet<Player>>();

            context.Setup(c => c.Players).Returns(players.Object);

            var viewModel = new NewAirlineViewModel(context.Object, data.Object);

            const string name = "Dave Blow";

            viewModel.Name = name;

            Assert.Equal(viewModel.Name, name);
        }
    }
}
