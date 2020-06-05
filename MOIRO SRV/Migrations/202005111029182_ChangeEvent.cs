namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "StatusId", "dbo.Status");
            DropIndex("dbo.Events", new[] { "StatusId" });
            RenameColumn(table: "dbo.Events", name: "StatusId", newName: "Status_Id");
            AddColumn("dbo.Events", "IsCanceled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Events", "Status_Id", c => c.Int());
            CreateIndex("dbo.Events", "Status_Id");
            AddForeignKey("dbo.Events", "Status_Id", "dbo.Status", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Status_Id", "dbo.Status");
            DropIndex("dbo.Events", new[] { "Status_Id" });
            AlterColumn("dbo.Events", "Status_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Events", "IsCanceled");
            RenameColumn(table: "dbo.Events", name: "Status_Id", newName: "StatusId");
            CreateIndex("dbo.Events", "StatusId");
            AddForeignKey("dbo.Events", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
    }
}
