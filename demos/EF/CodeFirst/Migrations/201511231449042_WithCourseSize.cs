namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithCourseSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "Size", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course", "Size");
        }
    }
}
