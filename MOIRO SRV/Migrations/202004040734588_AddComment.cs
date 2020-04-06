namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AdminComment", c => c.String());
            AddColumn("dbo.Orders", "CompletionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CompletionDate");
            DropColumn("dbo.Orders", "AdminComment");
        }
    }
}
