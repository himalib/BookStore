namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookModels", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookModels", "CategoryId");
        }
    }
}
