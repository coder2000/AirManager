using System.ComponentModel.Composition;
using System.Data.Entity.Migrations;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using AirManager.Infrastructure.Migrations;

namespace AirManager.Airlines.ViewModels
{
    [Export]
    public class AirlineTabViewModel : BindableBase
    {
        public AirlineTabViewModel()
        {
            UpdateDbCommand = new DelegateCommand(OnUpdateDb);
        }

        public ICommand UpdateDbCommand { get; private set; }

        private static void OnUpdateDb()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);

            migrator.Update();
        }
    }
}
