namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAuthors", "BookModel_Id", "dbo.BookModels");
            DropIndex("dbo.BookAuthors", new[] { "BookModel_Id" });
            DropTable("dbo.BookAuthors");
            DropTable("dbo.BookModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BookName = c.String(),
                        ISBN = c.Int(nullable: false),
                        Admin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        Admin = c.String(),
                        BookModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.BookAuthors", "BookModel_Id");
            AddForeignKey("dbo.BookAuthors", "BookModel_Id", "dbo.BookModels", "Id");
        }
    }
}
