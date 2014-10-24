using System;
using System.ComponentModel.Composition;
using System.Linq;
using AirManager.Infrastructure;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Menus.ViewModels
{
    [Export(typeof (LoadingViewModel))]
    public class LoadingViewModel : BindableBase
    {
        private static GameData _data;
        private readonly AirManagerContext _context;
        private readonly IRegionManager _regionManager;
        private string _message;
        private int _progress;

        [ImportingConstructor]
        public LoadingViewModel(IRegionManager regionManager, AirManagerContext context, GameData data)
        {
            _context = context;
            _data = data;
            _regionManager = regionManager;
        }

        public int LoadingProgress
        {
            get { return _progress; }
            set
            {
                if (_progress == value)
                {
                    return;
                }

                _progress = value;
                OnPropertyChanged(() => LoadingProgress);
            }
        }

        public string LoadingMessage
        {
            get { return _message; }
            set
            {
                if (_message == value)
                {
                    return;
                }

                _message = value;
                OnPropertyChanged(() => LoadingMessage);
            }
        }

        public void Loaded()
        {
            LoadingProgress = 20;
            LoadingMessage = "Loading Countries...";

            _data.Countries =
                (from country in _context.Countries.AsParallel() orderby country.Name select country).ToArray();

            LoadingProgress = 100;
            LoadingMessage = "Loading Completed!";

            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("NewAirline", UriKind.Relative));
        }
    }
}