// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition;
using System.Windows;
using AirManager.Infrastructure.Events;
using AirManager.Splash.ViewModels;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AirManager.Splash.Views
{
    /// <summary>
    ///     Interaction logic for LoadingView.xaml
    /// </summary>
    [Export]
    public partial class LoadingView
    {
        [ImportingConstructor]
        public LoadingView(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<CloseSplashEvent>().Subscribe((i) => Close());

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ViewModel.Loaded();
        }

        [Import]
        public LoadingViewModel ViewModel
        {
            private get { return DataContext as LoadingViewModel; }
            set { DataContext = value; }
        }
    }
}