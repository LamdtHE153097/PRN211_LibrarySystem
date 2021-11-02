namespace LibraryAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addpostsmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        id_post = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 255),
                        content = c.String(),
                        id_publisher = c.Int(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        id_user = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_post)
                .ForeignKey("dbo.Publishers", t => t.id_publisher, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.id_user, cascadeDelete: true)
                .Index(t => t.id_publisher)
                .Index(t => t.id_user);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "id_user", "dbo.Users");
            DropForeignKey("dbo.Posts", "id_publisher", "dbo.Publishers");
            DropIndex("dbo.Posts", new[] { "id_user" });
            DropIndex("dbo.Posts", new[] { "id_publisher" });
            DropTable("dbo.Posts");
        }
    }
}
