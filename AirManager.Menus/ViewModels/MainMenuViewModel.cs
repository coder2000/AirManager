using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Menus.ViewModels
{
    [Export]
    public class MainMenuViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly DelegateCommand<object> _newGameCommand;
        private readonly DelegateCommand<object> _loadGameCommand;

        [ImportingConstructor]
        public MainMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            _newGameCommand = new DelegateCommand<object>(NewGame);
            _loadGameCommand = new DelegateCommand<object>(LoadGame);
        }

        public ICommand NewGameCommand
        {
            get { return _newGameCommand; }
        }

        public ICommand LoadGameCommand
        {
            get { return _loadGameCommand; }
        }

        private void NewGame(object ignored)
        {
            
        }

        private void LoadGame(object ignored)
        {
            
        }

    }
}
