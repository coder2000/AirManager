using System;
using System.Collections.Generic;
using System.Globalization;

namespace AirManager.Infrastructure.Localization
{
    public abstract class LanguageDictionary
    {
        private static readonly Dictionary<CultureInfo, LanguageDictionary> RegisteredDictionaries = new Dictionary<CultureInfo, LanguageDictionary>();

        protected abstract string CultureName { get; }

        public CultureInfo Culture
        {
            get { return CultureInfo.GetCultureInfo(CultureName); }
        }

        public static LanguageDictionary Current
        {
            get { return GetDictionary(LanguageContext.Instance.Culture); }
        }

        public static void RegisterDictionary(CultureInfo culture, LanguageDictionary dictionary)
        {
            if (!RegisteredDictionaries.ContainsKey(culture))
            {
                RegisteredDictionaries.Add(culture, dictionary);
            }
        }

        public static void UnregisterDictionary(CultureInfo culture)
        {
            if (RegisteredDictionaries.ContainsKey(culture))
            {
                RegisteredDictionaries.Remove(culture);
            }
        }

        public T Translate<T>(string key, string valueId)
        {
            return (T) Translate(key, valueId, null, typeof (T));
        }

        public object Translate(string key, string valueId, object defaultValue, Type type)
        {
            return OnTranslate(key, valueId, defaultValue, type);
        }

        public static LanguageDictionary GetDictionary(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            LanguageDictionary dictionary;

            return RegisteredDictionaries.TryGetValue(culture, out dictionary) ? dictionary : null;
        }

        public void Load()
        {
            OnLoad();
        }

        public void Unload()
        {
            OnUnload();
        }

        protected abstract void OnLoad();
        protected abstract void OnUnload();
        protected abstract object OnTranslate(string key, string valueId, object defaultValue, Type type);
    }
}
