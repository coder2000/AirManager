// Copyright 2014 Dieter Lunn All Rights Reserved

using System.Windows;

namespace AirManager
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new AirBootstrapper();
            bootstrapper.Run();
        }
    }
}