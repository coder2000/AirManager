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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _changeLanguageCommand = new DelegateCommand<string>(ChangeLanguage);
            _exitCommand = new DelegateCommand<object>(ExitGame);
        }

        /// <summary>
        /// Gets the change language command.
        /// </summary>
        /// <value>
        /// The change language command.
        /// </value>
        public ICommand ChangeLanguageCommand
        {
            get { return _changeLanguageCommand; }
        }

        /// <summary>
        /// Gets the exit game command.
        /// </summary>
        /// <value>
        /// The exit game command.
        /// </value>
        public ICommand ExitGameCommand
        {
            get { return _exitCommand; }
        }

        /// <summary>
        /// Gets or sets the flag path.
        /// </summary>
        /// <value>
        /// The flag path.
        /// </value>
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

        /// <summary>
        /// Gets the cultures.
        /// </summary>
        /// <value>
        /// The cultures.
        /// </value>
        public ObservableCollection<CultureViewModel> Cultures
        {
            get { return _culture ?? (_culture = BuildCultures()); }
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        /// <param name="ignored">The ignored.</param>
        private void ExitGame(object ignored)
        {
            _eventAggregator.GetEvent<ExitGameEvent>().Publish(null);
        }

        /// <summary>
        /// Changes the language.
        /// </summary>
        /// <param name="langugage">The langugage.</param>
        private void ChangeLanguage(string langugage)
        {
            FlagPath = langugage;
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo(langugage);
        }

        /// <summary>
        /// Builds the cultures.
        /// </summary>
        /// <returns></returns>
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