namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookModels", "bookcategory_ID", "dbo.BookCategories");
            DropIndex("dbo.BookModels", new[] { "bookcategory_ID" });
            DropColumn("dbo.BookModels", "bookcategory_ID");
            DropTable("dbo.BookCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BookModels", "bookcategory_ID", c => c.Int());
            CreateIndex("dbo.BookModels", "bookcategory_ID");
            AddForeignKey("dbo.BookModels", "bookcategory_ID", "dbo.BookCategories", "ID");
        }
    }
}
