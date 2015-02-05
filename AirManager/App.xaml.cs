// Copyright 2014 Dieter Lunn All Rights Reserved

using System.Windows;

namespace AirManager
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new AirBootstrapper();
            bootstrapper.Run();
        }
    }
}