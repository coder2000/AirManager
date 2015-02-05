using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace AirManager.Boot.ViewModels
{
    public class InstallWindowViewModel : ViewModelBase
    {
        private RelayCommand _exitCommand;
        private RelayCommand _installCommand;
        private bool _installEnabled;
        private bool _isThinking;
        private RelayCommand _uninstallCommand;
        private bool _uninstallEnabled;

        public InstallWindowViewModel(BootstrapperApplication application)
        {
            IsThinking = false;
            Bootstrapper = application;
            Bootstrapper.ApplyComplete += BootstrapperOnApplyComplete;
            Bootstrapper.DetectPackageComplete += BootstrapperOnDetectPackageComplete;
            Bootstrapper.PlanComplete += BootstrapperOnPlanComplete;
        }

        public BootstrapperApplication Bootstrapper { get; private set; }

        public bool InstallEnabled
        {
            get { return _installEnabled; }
            set
            {
                _installEnabled = value;
                RaisePropertyChanged("InstallEnabled");
            }
        }

        public bool UninstallEnabled
        {
            get { return _uninstallEnabled; }
            set
            {
                _uninstallEnabled = value;
                RaisePropertyChanged("UninstallEnabled");
            }
        }

        public bool IsThinking
        {
            get { return _isThinking; }
            set
            {
                _isThinking = value;
                RaisePropertyChanged("IsThinking");
            }
        }

        public RelayCommand InstallCommand
        {
            get
            {
                return _installCommand ??
                       (_installCommand = new RelayCommand(InstallExecute, () => InstallEnabled = true));
            }
        }

        public RelayCommand UninstallCommand
        {
            get
            {
                return _uninstallCommand ??
                       (_uninstallCommand = new RelayCommand(UninstallExecute, () => UninstallEnabled = true));
            }
        }

        public RelayCommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new RelayCommand(ExitExecute)); }
        }

        private void ExitExecute()
        {
            AirManagerBA.BootstrapperDispatcher.InvokeShutdown();
        }

        private void UninstallExecute()
        {
            IsThinking = true;
            Bootstrapper.Engine.Plan(LaunchAction.Uninstall);
        }

        private void InstallExecute()
        {
            IsThinking = true;
            Bootstrapper.Engine.Plan(LaunchAction.Install);
        }

        private void BootstrapperOnPlanComplete(object sender, PlanCompleteEventArgs planCompleteEventArgs)
        {
            if (planCompleteEventArgs.Status >= 0)
            {
                Bootstrapper.Engine.Apply(IntPtr.Zero);
            }
        }

        private void BootstrapperOnDetectPackageComplete(object sender,
            DetectPackageCompleteEventArgs detectPackageCompleteEventArgs)
        {
            if (detectPackageCompleteEventArgs.PackageId == "AirManagerInstaller")
            {
                if (detectPackageCompleteEventArgs.State == PackageState.Absent)
                {
                    InstallEnabled = true;
                }
                else if (detectPackageCompleteEventArgs.State == PackageState.Present)
                {
                    UninstallEnabled = true;
                }
            }
        }

        private void BootstrapperOnApplyComplete(object sender, ApplyCompleteEventArgs applyCompleteEventArgs)
        {
            IsThinking = false;
            InstallEnabled = false;
            UninstallEnabled = false;
        }
    }
}