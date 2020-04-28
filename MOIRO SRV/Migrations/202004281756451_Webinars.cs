namespace MOIRO_SRV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Webinars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Webinars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameWebinar = c.String(nullable: false),
                        Description = c.String(),
                        Place = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        PlatformId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SessionLimit = c.Int(nullable: false),
                        ListenersCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebinarSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebCamerasCount = c.Int(nullable: false),
                        MaxWebinarsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Webinars", "UserId", "dbo.Users");
            DropForeignKey("dbo.Webinars", "PlatformId", "dbo.Platforms");
            DropIndex("dbo.Webinars", new[] { "PlatformId" });
            DropIndex("dbo.Webinars", new[] { "UserId" });
            DropTable("dbo.WebinarSettings");
            DropTable("dbo.Platforms");
            DropTable("dbo.Webinars");
        }
    }
}
