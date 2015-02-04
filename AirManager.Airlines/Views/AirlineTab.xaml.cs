using System.ComponentModel.Composition;
using AirManager.Airlines.ViewModels;

namespace AirManager.Airlines.Views
{
    /// <summary>
    ///     Interaction logic for AirlineTab.xaml
    /// </summary>
    [Export]
    public partial class AirlineTab
    {
        public AirlineTab()
        {
            InitializeComponent();
        }

        [Import]
        public AirlineTabViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}