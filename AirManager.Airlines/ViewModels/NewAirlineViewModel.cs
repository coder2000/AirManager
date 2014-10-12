using System.ComponentModel.Composition;
using System.Linq;
using AirManager.Infrastructure;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AirManager.Airlines.ViewModels
{
    [Export]
    public class NewAirlineViewModel : BindableBase
    {
        private readonly IOrderedQueryable<Country> _countries;
        private readonly AirManagerContext _dbContext = new AirManagerContext();
        private readonly IEventAggregator _eventAggregator;
        private string _countryId;

        [ImportingConstructor]
        public NewAirlineViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _countries = from country in _dbContext.Countries orderby country.Name select country;
        }

        public Country[] Countries
        {
            get { return _countries.ToArray(); }
        }

        public string CountryId
        {
            get { return _countryId; }
            set
            {
                if (_countryId == value) return;
                _countryId = value;
                OnPropertyChanged("CountryId");
            }
        }
    }
}