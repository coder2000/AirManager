// Copyright 2014 Dieter Lunn All Rights Reserved

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using AirManager.Infrastructure;
using AirManager.Infrastructure.Events;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AirManager.Splash.ViewModels
{
    [Export(typeof (LoadingViewModel))]
    public class LoadingViewModel : BindableBase, IDisposable
    {
        private static GameData _data;
        private readonly AirManagerContext _context;
        private readonly IEventAggregator _eventAggregator;
        private string _message;
        private int _progress;

        [ImportingConstructor]
        public LoadingViewModel(IEventAggregator eventAggregator, AirManagerContext context, GameData data)
        {
            _context = context;
            _data = data;
            _eventAggregator = eventAggregator;
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

            _eventAggregator.GetEvent<CloseSplashEvent>().Publish(null);
        }

        private void UpdateProgress(LoadProgress progress)
        {
            LoadingProgress = progress.Progress;
            LoadingMessage = progress.Message;
        }
    }
}