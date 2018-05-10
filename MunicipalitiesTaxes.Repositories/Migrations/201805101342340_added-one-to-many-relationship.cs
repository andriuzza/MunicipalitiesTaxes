namespace MunicipalitiesTaxes.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Addedonetomanyrelationship : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Municipality", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Municipality", "Name", c => c.String());
        }
    }
}
