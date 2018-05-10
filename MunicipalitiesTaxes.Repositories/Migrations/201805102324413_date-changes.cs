namespace MunicipalitiesTaxes.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datechanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tax", "Date", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tax", "Date", c => c.DateTime(nullable: false));
        }
    }
}
