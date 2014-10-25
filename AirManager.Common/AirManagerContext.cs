using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using AirManager.Infrastructure.Models;

namespace AirManager.Infrastructure
{
    [Export]
    public class AirManagerContext : DbContext
    {
        public AirManagerContext() : base(DatabaseConfig.ConnectionString)
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<City> Cities { get; set; } 
    }

    internal static class DatabaseConfig
    {
        private static string _connString;

        public static string ConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(_connString))
                {
                    _connString = CreateString();
                }

                return _connString;
            }
        }

        private static string CreateString()
        {
            string dataDir = "DataSource=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                             @"\AirManager\AirManager.sdf;password=!w6sMUcEU";
            return dataDir;
        }
    }
}