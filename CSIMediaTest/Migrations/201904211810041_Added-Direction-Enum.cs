namespace CSIMediaTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDirectionEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sequences", "Direction", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sequences", "Direction", c => c.String(nullable: false));
        }
    }
}
