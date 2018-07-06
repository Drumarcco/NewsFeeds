namespace NewsFeeds.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        PostedAt = c.DateTime(nullable: false),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        TopicName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Topics", t => t.TopicName, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.TopicName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "TopicName", "dbo.Topics");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "TopicName" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropTable("dbo.Posts");
        }
    }
}
