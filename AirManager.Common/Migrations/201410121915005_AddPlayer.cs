using System.Data.Entity.Migrations;

namespace AirManager.Infrastructure.Migrations
{
    public partial class AddPlayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airlines",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(maxLength: 4000),
                    Ceo = c.String(maxLength: 4000),
                    CountryId = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);

            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Id = c.String(false, 4000),
                    Name = c.String(maxLength: 4000),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Int(false, true),
                    AirlineId = c.Int(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Airlines", t => t.AirlineId, true)
                .Index(t => t.AirlineId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Players", "AirlineId", "dbo.Airlines");
            DropForeignKey("dbo.Airlines", "CountryId", "dbo.Countries");
            DropIndex("dbo.Players", new[] {"AirlineId"});
            DropIndex("dbo.Airlines", new[] {"CountryId"});
            DropTable("dbo.Players");
            DropTable("dbo.Countries");
            DropTable("dbo.Airlines");
        }
    }
}