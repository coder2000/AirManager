using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using AirManager.Infrastructure.Interfaces;
using AirManager.Splash.Views;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace AirManager.Splash
{
    [ModuleExport(typeof (SplashModule))]
    public class SplashModule : IModule
    {
        private readonly IShell _shell;
        private IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public SplashModule(IEventAggregator eventAggregator, IShell shell)
        {
            _eventAggregator = eventAggregator;
            _shell = shell;
        }

        public void Initialize()
        {
            Task startSplash = Task.Factory.StartNew(() =>
            {
                var showShell = new Task(() => _shell.Show());

                var showSplash = new Task(() =>
                {
                    var splash = ServiceLocator.Current.GetInstance<LoadingView>();
                    splash.Show();
                });

                showSplash.Start();
                showShell.Start();
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}