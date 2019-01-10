namespace AuthnAuthz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        ISBN = c.Int(nullable: false),
                        Admin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookModels");
        }
    }
}
