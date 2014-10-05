using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Menus
{
    [ModuleExport(typeof(MenuModule))]
    public class MenuModule : IModule
    {
        [Import]
        public IRegionManager RegionManager;

        public void Initialize()
        {
            
        }
    }
}
