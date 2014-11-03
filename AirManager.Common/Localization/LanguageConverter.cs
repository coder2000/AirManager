using System;
using System.Globalization;
using System.Windows.Data;

namespace AirManager.Infrastructure.Localization
{
    public class LanguageConverter : IValueConverter, IMultiValueConverter
    {
        private readonly object _defaultValue;
        private readonly string _valueId;
        private bool _isStatic;
        private string _key;

        public LanguageConverter(string key, string valueId, object defaultValue)
        {
            _key = key;
            _valueId = valueId;
            _defaultValue = defaultValue;
            _isStatic = true;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int parameterCount = _isStatic ? values.Length - 1 : values.Length - 2;
            if (string.IsNullOrEmpty(_key))
            {
                if (values[1] == null)
                {
                    throw new ArgumentNullException("values[1]");
                }

                _isStatic = false;
                _key = values[1].ToString();
                --parameterCount;
            }

            LanguageDictionary dictionary = ResolveDictionary();
            object translated = dictionary.Translate(_key, _valueId, _defaultValue, targetType);
            if (translated != null && parameterCount != 0)
            {
                var parameters = new object[parameterCount];
                Array.Copy(values, values.Length - parameterCount, parameters, 0, parameters.Length);

                translated = string.Format(translated.ToString(), parameters);
            }

            return translated;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[0];
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LanguageDictionary dictionary = ResolveDictionary();
            object translation = dictionary.Translate(_key, _valueId, _defaultValue, targetType);

            return translation;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        private LanguageDictionary ResolveDictionary()
        {
            LanguageDictionary dictionary = LanguageDictionary.GetDictionary(LanguageContext.Instance.Culture);
            if (dictionary == null)
            {
                throw new InvalidOperationException(string.Format("Dictionary for language {0} was not found",
                    LanguageContext.Instance.Culture));
            }

            return dictionary;
        }
    }
}