namespace JobTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Org_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Org_Id1", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Org_Id1");
            AddForeignKey("dbo.AspNetUsers", "Org_Id1", "dbo.Orgs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Org_Id1", "dbo.Orgs");
            DropIndex("dbo.AspNetUsers", new[] { "Org_Id1" });
            DropColumn("dbo.AspNetUsers", "Org_Id1");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "Org_Id");
        }
    }
}
