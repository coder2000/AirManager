using System.Windows.Threading;
using AirManager.Boot.ViewModels;
using AirManager.Boot.Views;
using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace AirManager.Boot
{
// ReSharper disable once InconsistentNaming
    public class AirManagerBA : BootstrapperApplication
    {
        public static Dispatcher BootstrapperDispatcher { get; private set; }

        protected override void Run()
        {
            Engine.Log(LogLevel.Verbose, "Launching custom WiX BA for AirManager installation");
            BootstrapperDispatcher = Dispatcher.CurrentDispatcher;

            var viewModel = new InstallWindowViewModel(this);
            viewModel.Bootstrapper.Engine.Detect();

            var window = new InstallWindow {DataContext = viewModel};
            window.Closed += (sender, args) => BootstrapperDispatcher.InvokeShutdown();
            window.Show();

            Dispatcher.Run();

            Engine.Quit(0);
        }
    }
}
