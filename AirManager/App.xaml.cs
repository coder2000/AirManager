// Copyright 2014 Dieter Lunn All Rights Reserved

using System.Globalization;
using System.Windows;
using AirManager.Infrastructure.Localization;

namespace AirManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LanguageDictionary.RegisterDictionary(CultureInfo.GetCultureInfo("en-US"), XmlLanguageDictionary.FromResource("AirManager.Resources.Languages.en-US.xml"));
            LanguageDictionary.RegisterDictionary(CultureInfo.GetCultureInfo("en-CA"), XmlLanguageDictionary.FromResource("AirManager.Resources.Languages.en-CA.xml"));
            LanguageDictionary.RegisterDictionary(CultureInfo.GetCultureInfo("fr-CA"), XmlLanguageDictionary.FromResource("AirManager.Resources.Languages.fr-CA.xml"));

            LanguageContext.Instance.Culture = CultureInfo.GetCultureInfo("fr-CA");

            var bootstrapper = new AirBootstrapper();
            bootstrapper.Run();
        }
    }
}
