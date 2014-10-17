using System.ComponentModel.Composition;
using System.Windows;
using AirManager.Menus.ViewModels;

namespace AirManager.Menus.Views
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

            Loaded += LoadingView_Loaded;
        }

        [Import]
        public LoadingViewModel ViewModel
        {
            get { return DataContext as LoadingViewModel; }
            set { DataContext = value; }
        }

        private void LoadingView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadedCommand.Execute(null);
        }
    }
}