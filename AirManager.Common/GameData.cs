using System.ComponentModel.Composition;
using AirManager.Infrastructure.Models;

namespace AirManager.Infrastructure
{
    [Export(typeof (GameData))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GameData
    {
        public Country[] Countries { get; set; }
    }
}