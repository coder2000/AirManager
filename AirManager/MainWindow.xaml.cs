// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel.Composition;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace AirManager
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : IPartImportsSatisfiedNotification
    {
        [Import(AllowRecomposition = false)] public IModuleManager ModuleManager;

        [Import(AllowRecomposition = false)] public IRegionManager RegionManager;

        [ImportingConstructor]
        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<ExitGameEvent>().Subscribe(ExitGame);
        }

        public void OnImportsSatisfied()
        {
            ModuleManager.LoadModuleCompleted += (s, e) =>
            {
                if (e.ModuleInfo.ModuleName == "MenuModule")
                {
                    RegionManager.RequestNavigate(RegionNames.MainRegion, new Uri("Loading", UriKind.Relative));
                }
            };
        }

        private void ExitGame(object obj)
        {
            Close();
        }
    }
}