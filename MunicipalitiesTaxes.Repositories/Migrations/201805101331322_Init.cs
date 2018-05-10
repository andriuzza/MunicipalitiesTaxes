namespace MunicipalitiesTaxes.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Municipality",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaxType = c.Int(nullable: false),
                        TaxDecimal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        MunicipalityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Municipality", t => t.MunicipalityId, cascadeDelete: true)
                .Index(t => t.MunicipalityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tax", "MunicipalityId", "dbo.Municipality");
            DropIndex("dbo.Tax", new[] { "MunicipalityId" });
            DropTable("dbo.Tax");
            DropTable("dbo.Municipality");
        }
    }
}
