using System.Data.Entity.Migrations;

namespace AirManager.Infrastructure.Migrations
{
    public partial class AddAirport : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AirlinerAirlines");
            RenameTable("dbo.AirlinerAirlines", "AirlineAirliners");
            CreateTable(
                "dbo.Airports",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(maxLength: 4000),
                    IATA = c.String(maxLength: 4000),
                    ICAO = c.String(maxLength: 4000),
                    Latitude = c.Single(false),
                    Longitude = c.Single(false),
                    Altitude = c.Int(false),
                    Offset = c.Single(false),
                    DST = c.String(maxLength: 4000),
                    TimeZone = c.String(maxLength: 4000),
                    CountryId = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);

            AddColumn("dbo.Cities", "Latitude", c => c.Int(false));
            AddColumn("dbo.Cities", "Longitude", c => c.Int(false));
            AddColumn("dbo.Cities", "TimeZone", c => c.String(maxLength: 4000));
            AddPrimaryKey("dbo.AirlineAirliners", new[] {"Airline_Id", "Airliner_Id"});
        }

        public override void Down()
        {
            DropForeignKey("dbo.Airports", "CountryId", "dbo.Countries");
            DropIndex("dbo.Airports", new[] {"CountryId"});
            DropPrimaryKey("dbo.AirlineAirliners");
            DropColumn("dbo.Cities", "TimeZone");
            DropColumn("dbo.Cities", "Longitude");
            DropColumn("dbo.Cities", "Latitude");
            DropTable("dbo.Airports");
            AddPrimaryKey("dbo.AirlineAirliners", new[] {"Airliner_Id", "Airline_Id"});
            RenameTable("dbo.AirlineAirliners", "AirlinerAirlines");
        }
    }
}