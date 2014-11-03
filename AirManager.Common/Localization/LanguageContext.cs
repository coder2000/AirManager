using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using AirManager.Infrastructure.Annotations;

namespace AirManager.Infrastructure.Localization
{
    public sealed class LanguageContext : INotifyPropertyChanged
    {
        public static readonly LanguageContext Instance = new LanguageContext();

        private CultureInfo _culture;
        private LanguageDictionary _dictionary;

        public CultureInfo Culture
        {
            get { return _culture ?? (_culture = CultureInfo.CurrentUICulture); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (Equals(_culture, value))
                {
                    return;
                }

                if (_culture != null)
                {
                    LanguageDictionary currentDictionary = LanguageDictionary.GetDictionary(_culture);
                    currentDictionary.Unload();
                }

                _culture = value;
                LanguageDictionary newDictionary = LanguageDictionary.GetDictionary(_culture);
                Thread.CurrentThread.CurrentUICulture = _culture;
                newDictionary.Load();
                Dictionary = newDictionary;
                OnPropertyChanged();
            }
        }

        public LanguageDictionary Dictionary
        {
            get { return _dictionary; }
            set
            {
                if (value != null && value != _dictionary)
                {
                    _dictionary = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
