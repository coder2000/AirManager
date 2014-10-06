using System.ComponentModel.Composition;
using AirManager.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace AirManager
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : IPartImportsSatisfiedNotification
    {
        [Import(AllowRecomposition = false)] public IModuleManager ModuleManager;

        [Import(AllowRecomposition = false)] public IRegionManager RegionManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            ModuleManager.LoadModuleCompleted += (s, e) =>
                {
                    if (e.ModuleInfo.ModuleName == "MenuModule")
                    {
                        RegionManager.RequestNavigate(RegionNames.MainRegion, "/MainMenu");
                    }
                };
        }
    }
}