namespace CSIMediaTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTypeTimeTaken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sequences", "TimeTaken", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sequences", "TimeTaken");
        }
    }
}
