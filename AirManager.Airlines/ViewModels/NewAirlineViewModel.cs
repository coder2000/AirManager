using System.ComponentModel.Composition;
using System.Linq;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace AirManager.Airlines.ViewModels
{
    [Export]
    public class NewAirlineViewModel : BindableBase
    {
        private readonly IOrderedQueryable<Country> _countries;
        private readonly AirManagerContext _dbContext = new AirManagerContext();
        private readonly Player _player;

        [ImportingConstructor]
        public NewAirlineViewModel()
        {
            _player = new Player {Airline = new Airline()};
            _dbContext.Players.Add(_player);

            _countries = from country in _dbContext.Countries orderby country.Name select country;
        }

        public Country[] Countries
        {
            get { return _countries.ToArray(); }
        }

        public Country Country
        {
            get { return _player.Airline.Country; }
            set
            {
                if (_player.Airline.Country == value)
                {
                    return;
                }
                _player.Airline.Country = value;
                OnPropertyChanged(() => Country);
            }
        }

        public string Name
        {
            get { return _player.Airline.Name; }
            set
            {
                if (_player.Airline.Name == value) return;
                _player.Airline.Name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Ceo
        {
            get { return _player.Airline.Ceo; }
            set
            {
                if (_player.Airline.Ceo == value) return;
                _player.Airline.Ceo = value;
                OnPropertyChanged(() => Ceo);
            }
        }
    }
}