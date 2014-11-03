using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AirManager.Infrastructure.Localization
{
    [ContentProperty("Parameters")]
    public class Translate : MarkupExtension
    {
        public static readonly DependencyProperty KeyProperty = DependencyProperty.RegisterAttached("Key",
            typeof (string), typeof (Translate), new UIPropertyMetadata(string.Empty));

        private Collection<BindingBase> _parameters = new Collection<BindingBase>();
        private DependencyProperty _property;
        private DependencyObject _target;

        public Translate() { }

        public Translate(object defaultValue)
        {
            Default = defaultValue;
        }

        public object Default { private get; set; }

        public string Key { get; set; }

        public Collection<BindingBase> Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public static string GetKey(DependencyObject key)
        {
            return (string) key.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject key, string value)
        {
            key.SetValue(KeyProperty, value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof (IProvideValueTarget)) as IProvideValueTarget;
            if (service == null)
            {
                return this;
            }

            var property = service.TargetProperty as DependencyProperty;
            var target = service.TargetObject as DependencyObject;

            if (property == null || target == null)
            {
                return this;
            }

            _target = target;
            _property = property;

            return BindDictionary(serviceProvider);
        }

        private object BindDictionary(IServiceProvider serviceProvider)
        {
            string key = Key ?? GetKey(_target);
            string valueId = _property.Name;
            object value;

            var binding = new Binding("Dictionary") {Source = LanguageContext.Instance, Mode = BindingMode.TwoWay};
            var converter = new LanguageConverter(key, valueId, Default);

            if (!_parameters.Any())
            {
                binding.Converter = converter;
                value = binding.ProvideValue(serviceProvider);
            }
            else
            {
                var multiBinding = new MultiBinding {Mode = BindingMode.TwoWay, Converter = converter};
                multiBinding.Bindings.Add(binding);

                if (string.IsNullOrEmpty(key))
                {
                    var keyBinding = _parameters[0] as Binding;

                    if (keyBinding == null)
                    {
                        throw new ArgumentException("Key binding parameter must be the first, and of type binding.");
                    }
                }

                foreach (BindingBase bindingBase in _parameters)
                {
                    var parameter = (Binding) bindingBase;
                    multiBinding.Bindings.Add(parameter);
                }

                value = multiBinding.ProvideValue(serviceProvider);
            }

            return value;
        }
    }
}