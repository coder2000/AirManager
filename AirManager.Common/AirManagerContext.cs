using System;
using System.Data.Entity;
using AirManager.Infrastructure.Models;

namespace AirManager.Infrastructure
{
    public class AirManagerContext : DbContext
    {
        public AirManagerContext() : base(DatabaseConfig.ConnectionString)
        {
        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<City> Cities { get; set; } 
    }

    internal class DatabaseConfig
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