namespace JobTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.Events", "Position_Id", "dbo.Positions");
            DropIndex("dbo.Events", new[] { "Contact_Id" });
            DropIndex("dbo.Events", new[] { "Position_Id" });
            RenameColumn(table: "dbo.Contacts", name: "Organization_Id", newName: "Org_Id1");
            RenameColumn(table: "dbo.Positions", name: "Organization_Id", newName: "Org_Id1");
            RenameColumn(table: "dbo.Positions", name: "ContactPerson_Id", newName: "Contact_Id1");
            RenameColumn(table: "dbo.Events", name: "Organization_Id", newName: "Org_Id1");
            RenameIndex(table: "dbo.Contacts", name: "IX_Organization_Id", newName: "IX_Org_Id1");
            RenameIndex(table: "dbo.Positions", name: "IX_ContactPerson_Id", newName: "IX_Contact_Id1");
            RenameIndex(table: "dbo.Positions", name: "IX_Organization_Id", newName: "IX_Org_Id1");
            RenameIndex(table: "dbo.Events", name: "IX_Organization_Id", newName: "IX_Org_Id1");
            AddColumn("dbo.Contacts", "Org_Id", c => c.Int());
            AddColumn("dbo.Positions", "Org_Id", c => c.Int());
            AddColumn("dbo.Positions", "Contact_Id", c => c.Int());
            AddColumn("dbo.Events", "Org_Id", c => c.Int());
            AddColumn("dbo.Events", "Contact_Id1", c => c.Int());
            AddColumn("dbo.Events", "Position_Id1", c => c.Int());
            CreateIndex("dbo.Events", "Contact_Id1");
            CreateIndex("dbo.Events", "Position_Id1");
            AddForeignKey("dbo.Events", "Contact_Id1", "dbo.Contacts", "Id");
            AddForeignKey("dbo.Events", "Position_Id1", "dbo.Positions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Position_Id1", "dbo.Positions");
            DropForeignKey("dbo.Events", "Contact_Id1", "dbo.Contacts");
            DropIndex("dbo.Events", new[] { "Position_Id1" });
            DropIndex("dbo.Events", new[] { "Contact_Id1" });
            DropColumn("dbo.Events", "Position_Id1");
            DropColumn("dbo.Events", "Contact_Id1");
            DropColumn("dbo.Events", "Org_Id");
            DropColumn("dbo.Positions", "Contact_Id");
            DropColumn("dbo.Positions", "Org_Id");
            DropColumn("dbo.Contacts", "Org_Id");
            RenameIndex(table: "dbo.Events", name: "IX_Org_Id1", newName: "IX_Organization_Id");
            RenameIndex(table: "dbo.Positions", name: "IX_Org_Id1", newName: "IX_Organization_Id");
            RenameIndex(table: "dbo.Positions", name: "IX_Contact_Id1", newName: "IX_ContactPerson_Id");
            RenameIndex(table: "dbo.Contacts", name: "IX_Org_Id1", newName: "IX_Organization_Id");
            RenameColumn(table: "dbo.Events", name: "Org_Id1", newName: "Organization_Id");
            RenameColumn(table: "dbo.Positions", name: "Contact_Id1", newName: "ContactPerson_Id");
            RenameColumn(table: "dbo.Positions", name: "Org_Id1", newName: "Organization_Id");
            RenameColumn(table: "dbo.Contacts", name: "Org_Id1", newName: "Organization_Id");
            CreateIndex("dbo.Events", "Position_Id");
            CreateIndex("dbo.Events", "Contact_Id");
            AddForeignKey("dbo.Events", "Position_Id", "dbo.Positions", "Id");
            AddForeignKey("dbo.Events", "Contact_Id", "dbo.Contacts", "Id");
        }
    }
}
