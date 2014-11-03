using System;
using System.ComponentModel;
using System.Globalization;

namespace AirManager.Infrastructure.Localization
{
    public class LanguageProvider : ISupportInitialize
    {
        public Type DictionaryType { get; set; }
        public CultureInfo Culture { get; set; }
        public object Parameter { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            object instance = Activator.CreateInstance(DictionaryType, new[] {Parameter});
            var dictionary = instance as LanguageDictionary;
            LanguageDictionary.RegisterDictionary(Culture, dictionary);
        }
    }
}