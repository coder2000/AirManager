using System.ComponentModel.Composition;
using System.Windows.Input;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Menus.ViewModels
{
    [Export]
    public class MainMenuViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly DelegateCommand<object> _exitGameCommand;
        private readonly DelegateCommand<object> _loadGameCommand;
        private readonly DelegateCommand<object> _newGameCommand;
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public MainMenuViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            _newGameCommand = new DelegateCommand<object>(NewGame);
            _loadGameCommand = new DelegateCommand<object>(LoadGame);
            _exitGameCommand = new DelegateCommand<object>(ExitGame);
        }

        public ICommand NewGameCommand
        {
            get { return _newGameCommand; }
        }

        public ICommand LoadGameCommand
        {
            get { return _loadGameCommand; }
        }

        public ICommand ExitGameCommand
        {
            get { return _exitGameCommand; }
        }

        private void NewGame(object ignored)
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, "/NewAirlineView");
        }

        private void LoadGame(object ignored)
        {
        }

        private void ExitGame(object ignored)
        {
            _eventAggregator.GetEvent<ExitGameEvent>().Publish(null);
        }
    }
}