// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Extensions;
using AirManager.Infrastructure.Models;
using AirManager.Menus.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Moq;
using Xunit;

namespace AirManager.Menus.Tests.ViewModels
{
    public class LoadingViewModelFixture
    {
        [Fact, ExcludeFromCodeCoverage]
        public void WhenLoadingDoneNavigatesToNewAirline()
        {
            var data = new Mock<GameData>();

            var regionMock = new Mock<IRegion>();
            regionMock.Setup(
                x => x.RequestNavigate(new Uri("NewAirline", UriKind.Relative), It.IsAny<Action<NavigationResult>>()))
                .Callback<Uri, Action<NavigationResult>>((s, c) => c(new NavigationResult(null, true)))
                .Verifiable();

            var regionManager = new Mock<IRegionManager>();
            regionManager.Setup(x => x.Regions.ContainsRegionWithName(RegionNames.MainRegion)).Returns(true);
            regionManager.Setup(x => x.Regions[RegionNames.MainRegion]).Returns(regionMock.Object);

            var countries = new List<Country>
            {
                new Country {Id = "CA", Name = "Canada"},
                new Country {Id = "US", Name = "United States of America"}
            }.AsQueryable();

            var countryMock = new Mock<DbSet<Country>>();
            countryMock.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.Provider);
            countryMock.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.ElementType);
            countryMock.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.Expression);
            countryMock.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.GetEnumerator());

            var contextMock = new Mock<AirManagerContext>();
            contextMock.Setup(m => m.Countries).Returns(countryMock.Object);

            var viewModel = new LoadingViewModel(regionManager.Object, contextMock.Object, data.Object);

            viewModel.Loaded();

            regionMock.VerifyAll();
        }

        [Fact, ExcludeFromCodeCoverage]
        public void LoadingProgressShouldTriggerEvent()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.ShouldNotifyOn(p => p.LoadingProgress).When(p => p.LoadingProgress = 2);
        }

        [Fact, ExcludeFromCodeCoverage]
        public void CanGetAndSetLoadingProgress()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.LoadingProgress = 2;

            Assert.Equal(viewModel.LoadingProgress, 2);
        }

        [Fact, ExcludeFromCodeCoverage]
        public void LoadingMessageShouldTriggerEvent()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.ShouldNotifyOn(p => p.LoadingMessage).When(p => p.LoadingMessage = "Testing");
        }

        [Fact, ExcludeFromCodeCoverage]
        public void CanGetAndSetLoadingMessage()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.LoadingMessage = "Testing";

            Assert.Equal(viewModel.LoadingMessage, "Testing");
        }

        [Fact, ExcludeFromCodeCoverage]
        public void LoadingProgressShouldNotTriggerEvent()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.LoadingProgress = 2;
            
            viewModel.ShouldNotNotifyOn(p => p.LoadingProgress).When(p => p.LoadingProgress = 2);
        }

        [Fact, ExcludeFromCodeCoverage]
        public void LoadingMessageShouldNotTriggerEvent()
        {
            var regionManager = new Mock<IRegionManager>();
            var context = new Mock<AirManagerContext>();
            var data = new Mock<GameData>();

            var viewModel = new LoadingViewModel(regionManager.Object, context.Object, data.Object);

            viewModel.LoadingMessage = "Testing";

            viewModel.ShouldNotNotifyOn(p => p.LoadingMessage).When(p => p.LoadingMessage = "Testing");
        }
    }
}