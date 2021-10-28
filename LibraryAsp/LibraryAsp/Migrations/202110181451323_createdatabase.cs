namespace LibraryAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        id_book = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        author = c.String(nullable: false, maxLength: 255),
                        id_publisher = c.Int(nullable: false),
                        id_category = c.Int(nullable: false),
                        year_publish = c.Int(nullable: false),
                        price = c.Single(nullable: false),
                        description = c.String(),
                        image = c.String(nullable: false, maxLength: 255),
                        createdAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_book)
                .ForeignKey("dbo.Categories", t => t.id_category, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.id_publisher, cascadeDelete: true)
                .Index(t => t.id_publisher)
                .Index(t => t.id_category);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id_category = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.id_category);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        id_publisher = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.id_publisher);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id_role = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.id_role);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        id_role = c.Int(nullable: false, identity: true),
                        id_user = c.Int(nullable: false),
                        id_book = c.Int(nullable: false),
                        start_time = c.DateTime(nullable: false),
                        end_time = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_role)
                .ForeignKey("dbo.Books", t => t.id_book, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.id_user, cascadeDelete: true)
                .Index(t => t.id_user)
                .Index(t => t.id_book);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id_user = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 255),
                        password = c.String(nullable: false, maxLength: 255),
                        fullname = c.String(nullable: false, maxLength: 255),
                        address = c.String(nullable: false, maxLength: 255),
                        gender = c.Int(nullable: false),
                        phone = c.String(nullable: false, maxLength: 255),
                        birthday = c.DateTime(nullable: false),
                        id_role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_user)
                .ForeignKey("dbo.Roles", t => t.id_role, cascadeDelete: true)
                .Index(t => t.id_role);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "id_user", "dbo.Users");
            DropForeignKey("dbo.Users", "id_role", "dbo.Roles");
            DropForeignKey("dbo.Transactions", "id_book", "dbo.Books");
            DropForeignKey("dbo.Books", "id_publisher", "dbo.Publishers");
            DropForeignKey("dbo.Books", "id_category", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "id_role" });
            DropIndex("dbo.Transactions", new[] { "id_book" });
            DropIndex("dbo.Transactions", new[] { "id_user" });
            DropIndex("dbo.Books", new[] { "id_category" });
            DropIndex("dbo.Books", new[] { "id_publisher" });
            DropTable("dbo.Users");
            DropTable("dbo.Transactions");
            DropTable("dbo.Roles");
            DropTable("dbo.Publishers");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
