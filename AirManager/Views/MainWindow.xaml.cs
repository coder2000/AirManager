// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel.Composition;
using System.Windows;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Events;
using AirManager.Infrastructure.Interfaces;
using AirManager.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof (IShell))]
    public partial class MainWindow : IShell
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public MainWindow(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;

            InitializeComponent();
            eventAggregator.GetEvent<ExitGameEvent>().Subscribe((i) => Close());

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("Airline", UriKind.Relative));
        }

        [Import]
        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }
    }
}