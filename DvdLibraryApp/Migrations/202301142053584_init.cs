namespace DvdLibraryApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        // up method runs the migration
        public override void Up()
        {
            CreateTable(
                "dbo.DirectorEFs",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        DirectorName = c.String(),
                    })
                .PrimaryKey(t => t.DirectorId);

            CreateTable(
                "dbo.DvdEFs",
                c => new
                {
                    DvdId = c.Int(nullable: false, identity: true),
                    DirectorId = c.Int(nullable: false),
                    RatingId = c.Int(),
                    Title = c.String(),
                    ReleaseYear = c.String(),
                    Notes = c.String(),
                })
                .PrimaryKey(t => t.DvdId)
                .ForeignKey("dbo.DirectorEFs", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.RatingEFs", t => t.RatingId)
                .Index(t => t.DirectorId)
                .Index(t => t.RatingId);
            
            CreateTable(
                "dbo.RatingEFs",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        Rating = c.String(),
                    })
                .PrimaryKey(t => t.RatingId);
            
        }
        // down method reverts the migration
        public override void Down()
        {
            DropForeignKey("dbo.DvdEFs", "RatingId", "dbo.RatingEFs");
            DropForeignKey("dbo.DvdEFs", "DirectorId", "dbo.DirectorEFs");
            DropIndex("dbo.DvdEFs", new[] { "RatingId" });
            DropIndex("dbo.DvdEFs", new[] { "DirectorId" });
            DropTable("dbo.RatingEFs");
            DropTable("dbo.DvdEFs");
            DropTable("dbo.DirectorEFs");
        }
    }
}
