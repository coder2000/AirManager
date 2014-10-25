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

            IsVisibleChanged += OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible)
            {
                ViewModel.Loaded();
            }
        }

        [Import]
        public LoadingViewModel ViewModel
        {
            private get { return DataContext as LoadingViewModel; }
            set { DataContext = value; }
        }
    }
}