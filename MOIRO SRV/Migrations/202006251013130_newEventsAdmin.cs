namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newEventsAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "AdminId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "AdminId");
        }
    }
}
