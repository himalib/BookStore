namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookCategories", "Admin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookCategories", "Admin", c => c.String());
        }
    }
}
