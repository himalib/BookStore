namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Admin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BookModels", "bookcategory_ID", c => c.Int());
            CreateIndex("dbo.BookModels", "bookcategory_ID");
            AddForeignKey("dbo.BookModels", "bookcategory_ID", "dbo.BookCategories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookModels", "bookcategory_ID", "dbo.BookCategories");
            DropIndex("dbo.BookModels", new[] { "bookcategory_ID" });
            DropColumn("dbo.BookModels", "bookcategory_ID");
            DropTable("dbo.BookCategories");
        }
    }
}
