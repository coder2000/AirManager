using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AirManager.Airlines.ViewModels
{
    [Export]
    public class NewAirlineViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public NewAirlineViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
