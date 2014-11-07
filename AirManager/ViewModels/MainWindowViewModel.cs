using System.Collections.ObjectModel;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly DelegateCommand<object> _exitCommand;
        private ObservableCollection<CultureViewModel> _culture;
        private string _languageCode = "en";

        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _changeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            _exitCommand = new DelegateCommand<object>(ExitGame);
        }

        public ICommand ChangeLanguageCommand
        {
            get { return _changeLanguageCommand; }
        }

        public ICommand ExitGameCommand
        {
            get { return _exitCommand; }
        }

        public string FlagPath
        {
            get
            {
                return string.Format("pack://application:,,,/AirManager;component/Resources/Icons/Flags/{0}.png",
                    _languageCode);
            }

            set
            {
                _languageCode = value;
                OnPropertyChanged(() => FlagPath);
            }
        }

        public ObservableCollection<CultureViewModel> Cultures
        {
            get { return _culture ?? (_culture = BuildCultures()); }
        }

        private void ExitGame(object ignored)
        {
            _eventAggregator.GetEvent<ExitGameEvent>().Publish(null);
        }

        private void ChangeLanguage(string langugage)
        {
            FlagPath = langugage;
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(langugage);
        }

        private static ObservableCollection<CultureViewModel> BuildCultures()
        {
            var cultures = new ObservableCollection<CultureViewModel>
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