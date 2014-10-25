using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace AirManager.Airlines.ViewModels
{
    [Export]
    public class NewAirlineViewModel : BindableBase, IDisposable
    {
        private readonly AirManagerContext _context;
        private readonly Player _player;
        private readonly GameData _data;

        [ImportingConstructor]
        public NewAirlineViewModel(AirManagerContext context, GameData data)
        {
            _data = data;
            _context = context;
            _player = new Player {Airline = new Airline()};
            _context.Players.Add(_player);    
        }

        public IEnumerable<Country> Countries
        {
            get { return _data.Countries; }
        }

        public Country Country
        {
            get { return _player.Airline.Country; }
            set
            {
                if (_player.Airline.Country == value) return;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}