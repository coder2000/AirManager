using System.ComponentModel.Composition;
using AirManager.Menus.ViewModels;

namespace AirManager.Menus.Views
{
    /// <summary>
    ///     Interaction logic for MainMenuView.xaml
    /// </summary>
    [Export("MainMenu")]
    public partial class MainMenuView
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public MainMenuViewModel ViewModel
        {
            get { return DataContext as MainMenuViewModel; }
            set { DataContext = value; }
        }
    }
}