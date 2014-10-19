using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using AirManager.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Menus.ViewModels
{
    [Export(typeof (LoadingViewModel))]
    public class LoadingViewModel : BindableBase
    {
        private static GameData _data;
        private static BackgroundWorker _worker;
        private readonly AirManagerContext _context = new AirManagerContext();
        private readonly DelegateCommand<object> _loadedCommand = new DelegateCommand<object>(Loaded);
        private readonly IRegionManager _regionManager;
        private string _message;
        private int _progress;

        [ImportingConstructor]
        public LoadingViewModel(IRegionManager regionManager, GameData data)
        {
            _data = data;
            _regionManager = regionManager;
            _worker = new BackgroundWorker {WorkerReportsProgress = true};
            _worker.ProgressChanged += WorkerOnProgressChanged;
            _worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;
            _worker.DoWork += WorkerOnDoWork;
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

        public DelegateCommand<object> LoadedCommand
        {
            get { return _loadedCommand; }
        }

        private static void Loaded(object ignored)
        {
            _worker.RunWorkerAsync();
        }

        private void WorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri("NewAirline", UriKind.Relative));
        }

        private void WorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadingProgress = e.ProgressPercentage;
            LoadingMessage = (string) e.UserState;
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _worker.ReportProgress(20, "Loading countries...");

            _data.Countries = (from country in _context.Countries orderby country.Name select country).ToArray();

            _worker.ReportProgress(100, "Loading completed!");
        }
    }
}