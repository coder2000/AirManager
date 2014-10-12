using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Airlines
{
    [ModuleExport(typeof (AirlineModule))]
    public class AirlineModule : IModule
    {
        [Import] public IRegionManager RegionManager;

        public void Initialize()
        {
        }
    }
}