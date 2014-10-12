using System.Data.Entity.Migrations;

namespace AirManager.Infrastructure.Migrations
{
    public partial class AddCitiesAndAirlines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(maxLength: 4000),
                    Population = c.Int(false),
                    CountryId = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);

            AddColumn("dbo.Airlines", "Iata", c => c.String(maxLength: 4000));
            AddColumn("dbo.Airlines", "Market", c => c.Int(false));
            AddColumn("dbo.Airlines", "Real", c => c.Boolean(false));
            AddColumn("dbo.Airlines", "Start", c => c.DateTime(false));
            AddColumn("dbo.Airlines", "End", c => c.DateTime(false));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] {"CountryId"});
            DropColumn("dbo.Airlines", "End");
            DropColumn("dbo.Airlines", "Start");
            DropColumn("dbo.Airlines", "Real");
            DropColumn("dbo.Airlines", "Market");
            DropColumn("dbo.Airlines", "Iata");
            DropTable("dbo.Cities");
        }
    }
}