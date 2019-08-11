namespace EventQuery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyIgTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "DateGenerated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformations", "DateGenerated");
        }
    }
}
