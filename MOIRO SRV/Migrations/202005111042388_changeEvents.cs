namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Status_Id", "dbo.Status");
            DropIndex("dbo.Events", new[] { "Status_Id" });
            DropColumn("dbo.Events", "Status_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Status_Id", c => c.Int());
            CreateIndex("dbo.Events", "Status_Id");
            AddForeignKey("dbo.Events", "Status_Id", "dbo.Status", "Id");
        }
    }
}
