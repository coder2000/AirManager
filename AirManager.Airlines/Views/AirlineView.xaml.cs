using System.ComponentModel.Composition;

namespace AirManager.Airlines.Views
{
    /// <summary>
    /// Interaction logic for AirlineView.xaml
    /// </summary>
    [Export("Airline")]
    public partial class AirlineView
    {
        public AirlineView()
        {
            InitializeComponent();
        }
    }
}
