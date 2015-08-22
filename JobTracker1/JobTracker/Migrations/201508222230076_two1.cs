namespace JobTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Event_Id", "dbo.Events");
            DropIndex("dbo.Contacts", new[] { "Event_Id" });
            AddColumn("dbo.Positions", "Title", c => c.String());
            AddColumn("dbo.Events", "Title", c => c.String());
            AddColumn("dbo.Events", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Contact_Id", c => c.Int());
            CreateIndex("dbo.Events", "Contact_Id");
            AddForeignKey("dbo.Events", "Contact_Id", "dbo.Contacts", "Id");
            DropColumn("dbo.Contacts", "Event_Id");
            DropColumn("dbo.Events", "Type");
            DropColumn("dbo.Events", "Initiated");
            DropColumn("dbo.Events", "FollowUp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "FollowUp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Initiated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Type", c => c.String());
            AddColumn("dbo.Contacts", "Event_Id", c => c.Int());
            DropForeignKey("dbo.Events", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.Events", new[] { "Contact_Id" });
            DropColumn("dbo.Events", "Contact_Id");
            DropColumn("dbo.Events", "Time");
            DropColumn("dbo.Events", "Date");
            DropColumn("dbo.Events", "Title");
            DropColumn("dbo.Positions", "Title");
            CreateIndex("dbo.Contacts", "Event_Id");
            AddForeignKey("dbo.Contacts", "Event_Id", "dbo.Events", "Id");
        }
    }
}
