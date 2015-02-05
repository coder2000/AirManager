// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition.Hosting;
using System.Windows;
using AirManager.Airlines;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Interfaces;
using AirManager.Infrastructure.Regions;
using AirManager.Splash;
using Fluent;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager
{
    /// <summary>
    /// </summary>
    public class AirBootstrapper : MefBootstrapper
    {
        private readonly AirLoggerAdapter _logger = new AirLoggerAdapter();

        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        ///     The shell of the application.
        /// </returns>
        /// <remarks>
        ///     If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        ///     <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default
        ///     <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        ///     the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" />
        ///     attached property
        ///     in order to be able to add regions by using the
        ///     <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        ///     attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<IShell>() as DependencyObject;
        }

        /// <summary>
        ///     Initializes the shell.
        /// </summary>
        /// <remarks>
        ///     The base implementation ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            //base.InitializeShell();
            //Application.Current.MainWindow = (Window) Shell;
            //Application.Current.MainWindow.Show();
        }

        /// <summary>
        ///     Configures the <see cref="P:Microsoft.Practices.Prism.MefExtensions.MefBootstrapper.AggregateCatalog" /> used by
        ///     MEF.
        /// </summary>
        /// <remarks>
        ///     The base implementation does nothing.
        /// </remarks>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (AirBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (SplashModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (InfrastructureModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof (AirlineModule).Assembly));
        }

        /// <summary>
        ///     Creates the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        ///     Create the <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade" /> used by the bootstrapper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///     The base implementation returns a new TextLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }

        /// <summary>
        ///     Configures the default region adapter mappings to use in the application, in order
        ///     to adapt UI controls defined in XAML to use a region and register it automatically.
        ///     May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings" /> instance containing all the mappings.
        /// </returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof (Ribbon), Container.GetExportedValue<RibbonRegionAdapter>());

            return mappings;
        }

        /// <summary>
        ///     Creates the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> that will be used as
        ///     the default container.
        /// </summary>
        /// <returns>
        ///     A new instance of <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" />.
        /// </returns>
        /// <remarks>
        ///     The base implementation registers a default MEF catalog of exports of key Prism types.
        ///     Exporting your own types will replace these defaults.
        /// </remarks>
        protected override CompositionContainer CreateContainer()
        {
            var container = new CompositionContainer(AggregateCatalog, CompositionOptions.DisableSilentRejection);

            return container;
        }
    }
}