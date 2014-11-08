// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using AirManager.Infrastructure;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.ViewModels
{
    [Export(typeof (LoadingViewModel))]
    public class LoadingViewModel : BindableBase, IDisposable
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
            private set
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

        public void Dispose()
        {
            _context.Dispose();
        }

        private async Task LoadData(IProgress<LoadProgress> progress)
        {
            progress.Report(new LoadProgress {Message = "Loading Countries...", Progress = 20});

            await Task.Run(() => (_data.Countries =
                (from country in _context.Countries orderby country.Name select country).ToArray()));

            progress.Report(new LoadProgress {Message = "Loading Completed!", Progress = 100});
        }

        public async void Loaded()
        {
            await LoadData(new Progress<LoadProgress>(UpdateProgress));

            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("NewAirline", UriKind.Relative));
        }

        private void UpdateProgress(LoadProgress progress)
        {
            LoadingProgress = progress.Progress;
            LoadingMessage = progress.Message;
        }
    }
}