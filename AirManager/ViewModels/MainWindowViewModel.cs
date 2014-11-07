using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Input;
using AirManager.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using WPFLocalizeExtension.Engine;

namespace AirManager.ViewModels
{
    [Export]
    public class MainWindowViewModel : BindableBase
    {
        private readonly DelegateCommand<string> _changeLanguageCommand;
        private readonly DelegateCommand<object> _exitCommand;
        private List<CultureViewModel> _culture;
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _changeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            _exitCommand = new DelegateCommand<object>(ExitGame);
        }

        private void ExitGame(object ignored)
        {
            _eventAggregator.GetEvent<ExitGameEvent>().Publish(null);
        }

        public ICommand ChangeLanguageCommand
        {
            get { return _changeLanguageCommand; }
        }

        public ICommand ExitGameCommand
        {
            get { return _exitCommand; }
        }

        public IEnumerable<CultureViewModel> Cultures
        {
            get { return _culture ?? (_culture = BuildCultures()); }
        }

        private void ChangeLanguage(string langugage)
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(langugage);
        }

        private static List<CultureViewModel> BuildCultures()
        {
            var cultures = new List<CultureViewModel>
            {
                CultureViewModel.Create(
                    string.Format("pack://application:,,,/AirManager;component/Resources/Icons/Flags/{0}.png", "en"),
                    "en", "English"),
                CultureViewModel.Create(
                    string.Format("pack://application:,,,/AirManager;component/Resources/Icons/Flags/{0}.png", "fr"),
                    "fr", "French")
            };

            return cultures;
        }
    }
}