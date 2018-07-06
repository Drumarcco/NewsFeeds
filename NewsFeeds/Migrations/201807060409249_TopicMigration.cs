namespace NewsFeeds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TopicName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.UserId, t.TopicName })
                .ForeignKey("dbo.Topics", t => t.TopicName, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TopicName);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "TopicName", "dbo.Topics");
            DropIndex("dbo.Subscriptions", new[] { "TopicName" });
            DropIndex("dbo.Subscriptions", new[] { "UserId" });
            DropTable("dbo.Topics");
            DropTable("dbo.Subscriptions");
        }
    }
}
