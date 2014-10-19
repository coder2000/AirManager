using System;
using AirManager.Infrastructure;
using AirManager.Menus.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Moq;
using Xunit;

namespace AirManager.Menus.Tests.ViewModels
{
    public class LoadingViewModelFixture
    {
        [Fact]
        public void WhenLoadingDoneNavigatesToNewAirline()
        {
            var data = new Mock<GameData>();

            var regionMock = new Mock<IRegion>();
            regionMock.Setup(
                x => x.RequestNavigate(new Uri("/NewAirline", UriKind.Relative), It.IsAny<Action<NavigationResult>>()))
                .Callback<Uri, Action<NavigationResult>>((s, c) => c(new NavigationResult(null, true)))
                .Verifiable();

            var regionManager = new Mock<IRegionManager>();
            regionManager.Setup(x => x.Regions.ContainsRegionWithName(RegionNames.MainRegion)).Returns(true);
            regionManager.Setup(x => x.Regions[RegionNames.MainRegion]).Returns(regionMock.Object);

            var viewModel = new LoadingViewModel(regionManager.Object, data.Object);

            viewModel.LoadedCommand.Execute(null);

            regionMock.VerifyAll();
        }
    }
}