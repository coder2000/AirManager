// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition;
using AirManager.Airlines.ViewModels;

namespace AirManager.Airlines.Views
{
    /// <summary>
    ///     Interaction logic for NewAirlineView.xaml
    /// </summary>
    [Export("NewAirline")]
    public partial class NewAirlineView
    {
        public NewAirlineView()
        {
            InitializeComponent();
        }

        [Import]
        public NewAirlineViewModel ViewModel
        {
            get { return DataContext as NewAirlineViewModel; }
            set { DataContext = value; }
        }
    }
}