namespace CSIMediaTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sequences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NewSequence = c.Int(nullable: false),
                        Direction = c.String(),
                        TimeTaken = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sequences");
        }
    }
}
