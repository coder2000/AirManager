﻿// Copyright 2014 Dieter Lunn All Rights Reserved

using System.Diagnostics.CodeAnalysis;
using AirManager.Menus.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Moq;
using Xunit;

namespace AirManager.Menus.Tests.ViewModels
{
    public class MainMenuViewModelFixture
    {
        [Fact]
        [ExcludeFromCodeCoverage]
        public void CanInitMainMenuViewModel()
        {
            var regionManager = new Mock<IRegionManager>();
            var eventAggregator = new Mock<IEventAggregator>();
            var viewModel = new MainMenuViewModel(regionManager.Object, eventAggregator.Object);

            Assert.NotNull(viewModel);
        }
    }
}