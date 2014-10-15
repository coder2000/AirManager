using System.Data.Entity.Migrations;

namespace AirManager.Infrastructure.Migrations
{
    public partial class AddAirliner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airliners",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(maxLength: 4000),
                    Manufacturer = c.String(maxLength: 4000),
                    Price = c.Int(false),
                    Type = c.Int(false),
                    Range = c.Int(false),
                    PassengerCount = c.Int(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AirlinerAirlines",
                c => new
                {
                    Airliner_Id = c.Int(false),
                    Airline_Id = c.Int(false),
                })
                .PrimaryKey(t => new {t.Airliner_Id, t.Airline_Id})
                .ForeignKey("dbo.Airliners", t => t.Airliner_Id, true)
                .ForeignKey("dbo.Airlines", t => t.Airline_Id, true)
                .Index(t => t.Airliner_Id)
                .Index(t => t.Airline_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AirlinerAirlines", "Airline_Id", "dbo.Airlines");
            DropForeignKey("dbo.AirlinerAirlines", "Airliner_Id", "dbo.Airliners");
            DropIndex("dbo.AirlinerAirlines", new[] {"Airline_Id"});
            DropIndex("dbo.AirlinerAirlines", new[] {"Airliner_Id"});
            DropTable("dbo.AirlinerAirlines");
            DropTable("dbo.Airliners");
        }
    }
}