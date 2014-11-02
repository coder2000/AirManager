// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition.Hosting;
using System.Windows;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Regions;
using Fluent;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager
{
    public class AirBootstrapper : MefBootstrapper
    {
        private readonly AirLoggerAdapter _logger = new AirLoggerAdapter();

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (AirBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (InfrastructureModule).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof (Ribbon), Container.GetExportedValue<RibbonRegionAdapter>());

            return mappings;
        }

        protected override CompositionContainer CreateContainer()
        {
            var container = new CompositionContainer(AggregateCatalog, CompositionOptions.DisableSilentRejection);

            return container;
        }
    }
}