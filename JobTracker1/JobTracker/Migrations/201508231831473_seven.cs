namespace JobTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seven : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Org_Id = c.Int(),
                        Org_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orgs", t => t.Org_Id1)
                .Index(t => t.Org_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Org_Id1", "dbo.Orgs");
            DropIndex("dbo.Profiles", new[] { "Org_Id1" });
            DropTable("dbo.Profiles");
        }
    }
}
