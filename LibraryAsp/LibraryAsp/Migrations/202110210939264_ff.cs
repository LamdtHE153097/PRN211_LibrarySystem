namespace LibraryAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transactions", new[] { "id_transaction" });
            DropColumn("dbo.Transactions", "id_role");
            AddColumn("dbo.Transactions", "id_transaction", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "id_transaction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "id_role", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Transactions");
            DropColumn("dbo.Transactions", "id_transaction");
            AddPrimaryKey("dbo.Transactions", "id_role");
        }
    }
}
