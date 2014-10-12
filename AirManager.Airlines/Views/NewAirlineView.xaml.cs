using System.ComponentModel.Composition;
using AirManager.Airlines.ViewModels;

namespace AirManager.Airlines.Views
{
    /// <summary>
    /// Interaction logic for NewAirlineView.xaml
    /// </summary>
    [Export("NewAirlineView")]
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
