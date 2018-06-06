namespace ContactInformation.DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerContacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                        ContactValue = c.String(),
                        ContactStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerContacts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerContacts", "ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.CustomerContacts", new[] { "ContactTypeId" });
            DropIndex("dbo.CustomerContacts", new[] { "CustomerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerContacts");
            DropTable("dbo.ContactTypes");
        }
    }
}
