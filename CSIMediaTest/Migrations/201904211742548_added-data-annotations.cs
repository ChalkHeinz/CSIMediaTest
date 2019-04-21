namespace CSIMediaTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddataannotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sequences", "NewSequence", c => c.String(nullable: false));
            AlterColumn("dbo.Sequences", "Direction", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sequences", "Direction", c => c.String());
            AlterColumn("dbo.Sequences", "NewSequence", c => c.String());
        }
    }
}
