using AirManager.Menus.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Moq;
using Xunit;

namespace AirManager.Menus.Tests.ViewModels
{
    public class MainMenuViewModelFixture
    {
        [Fact]
        public void CanInitViewModel()
        {
            var regionManager = new Mock<IRegionManager>();
            var viewModel = new MainMenuViewModel(regionManager.Object);

            Assert.NotNull(viewModel);
        }
    }
}
