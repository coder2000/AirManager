// Copyright 2014 Dieter Lunn All Rights Reserved

using System.ComponentModel.Composition;
using AirManager.Airlines.Views;
using AirManager.Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Airlines
{
    [ModuleExport(typeof (AirlineModule), DependsOnModuleNames = new []{"InfrastructureModule"})]
    public class AirlineModule : IModule
    {
        [Import] public IRegionManager RegionManager;

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof (AirlineTab));
        }
    }
}