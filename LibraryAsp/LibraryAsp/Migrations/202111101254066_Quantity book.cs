namespace LibraryAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Quantitybook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "quantity");
        }
    }
}
