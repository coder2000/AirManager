using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml;

namespace AirManager.Infrastructure.Localization
{
    public class XmlLanguageDictionary : LanguageDictionary
    {
        private readonly Dictionary<string, Dictionary<string, string>> _data =
            new Dictionary<string, Dictionary<string, string>>();

        private readonly Stream _stream;
        private string _cultureName;

        private XmlLanguageDictionary(Stream stream)
        {
            _stream = stream;
        }

        protected override string CultureName
        {
            get { return _cultureName; }
        }

        protected override void OnLoad()
        {
            var document = new XmlDocument();
            document.Load(_stream);

            if (document.DocumentElement == null) return;
            if (document.DocumentElement.Name != "Dictionary")
            {
                throw new XmlException("Invalid root element. Must be a dictionary.");
            }

            XmlAttribute cultureAttr = document.DocumentElement.Attributes["CultureName"];

            if (cultureAttr != null)
            {
                _cultureName = cultureAttr.Value;
            }

            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                if (node.Name != "Value") continue;
                var innerData = new Dictionary<string, string>();
                if (node.Attributes == null) continue;
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name == "Id")
                    {
                        if (!_data.ContainsKey(attribute.Value))
                        {
                            _data[attribute.Value] = innerData;
                        }
                    }
                    else
                    {
                        innerData[attribute.Name] = attribute.Value;
                    }
                }
            }
        }

        protected override void OnUnload()
        {
            _data.Clear();
        }

        protected override object OnTranslate(string key, string valueId, object defaultValue, Type type)
        {
            Dictionary<string, string> data = _data[key];

            if (_data.ContainsKey(key) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(valueId) ||
                !data.ContainsKey(valueId))
            {
                return defaultValue;
            }

            string textValue = data[valueId];

            if (type == typeof (object))
            {
                return textValue;
            }

            TypeConverter typeConverter = TypeDescriptor.GetConverter(type);

            object translation = typeConverter.ConvertFromString(textValue);

            return translation;
        }

        public static XmlLanguageDictionary FromResource(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return new XmlLanguageDictionary(stream);
        }
    }
}