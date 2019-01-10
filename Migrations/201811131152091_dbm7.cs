namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        Admin = c.String(),
                        BookModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookModels", t => t.BookModel_Id)
                .Index(t => t.BookModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookAuthors", "BookModel_Id", "dbo.BookModels");
            DropIndex("dbo.BookAuthors", new[] { "BookModel_Id" });
            DropTable("dbo.BookAuthors");
        }
    }
}
