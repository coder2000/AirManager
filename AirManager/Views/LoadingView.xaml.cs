// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition;
using System.Windows;
using AirManager.ViewModels;

namespace AirManager.Views
{
    /// <summary>
    ///     Interaction logic for LoadingView.xaml
    /// </summary>
    [Export("Loading")]
    public partial class LoadingView
    {
        public LoadingView()
        {
            InitializeComponent();

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