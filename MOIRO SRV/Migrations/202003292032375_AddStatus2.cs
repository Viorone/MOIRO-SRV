namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "StatusId", c => c.Int(nullable: true));
            AddColumn("dbo.Orders", "StatusId", c => c.Int(nullable: true));
            CreateIndex("dbo.Events", "StatusId");
            CreateIndex("dbo.Orders", "StatusId");
            AddForeignKey("dbo.Events", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Events", "StatusId", "dbo.Status");
            DropIndex("dbo.Orders", new[] { "StatusId" });
            DropIndex("dbo.Events", new[] { "StatusId" });
            DropColumn("dbo.Orders", "StatusId");
            DropColumn("dbo.Events", "StatusId");
        }
    }
}
