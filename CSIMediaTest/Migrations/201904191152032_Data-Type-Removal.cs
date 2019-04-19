namespace CSIMediaTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTypeRemoval : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sequences", "NewSequence", c => c.String());
            DropColumn("dbo.Sequences", "TimeTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sequences", "TimeTaken", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Sequences", "NewSequence", c => c.Int(nullable: false));
        }
    }
}
