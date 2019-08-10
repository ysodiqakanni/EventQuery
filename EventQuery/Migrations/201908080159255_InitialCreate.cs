namespace EventQuery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdFromEventbrite = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        End = c.String(),
                        Status = c.String(),
                        OrgId = c.String(),
                        OrganizationId = c.String(),
                        TicketAvailable = c.Boolean(nullable: false),
                        Venue = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInformations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        RetrievedUsing = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInformations");
            DropTable("dbo.TblEvents");
        }
    }
}
