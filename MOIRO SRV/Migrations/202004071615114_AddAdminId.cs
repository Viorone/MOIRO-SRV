namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "SecondUserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "SecondUserId" });
            RenameColumn(table: "dbo.Orders", name: "FirstUserId", newName: "UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_FirstUserId", newName: "IX_UserId");
            AddColumn("dbo.Orders", "AdminId", c => c.Int());
            DropColumn("dbo.Orders", "SecondUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "SecondUserId", c => c.Int());
            DropColumn("dbo.Orders", "AdminId");
            RenameIndex(table: "dbo.Orders", name: "IX_UserId", newName: "IX_FirstUserId");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "FirstUserId");
            CreateIndex("dbo.Orders", "SecondUserId");
            AddForeignKey("dbo.Orders", "SecondUserId", "dbo.Users", "Id");
        }
    }
}
