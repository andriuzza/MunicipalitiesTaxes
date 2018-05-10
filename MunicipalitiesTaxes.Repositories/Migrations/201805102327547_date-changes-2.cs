namespace MunicipalitiesTaxes.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datechanges2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tax", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tax", "Date", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
    }
}
