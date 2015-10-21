namespace Polling.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingStringLength : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Choice_Id", "dbo.Choices");
            DropIndex("dbo.Votes", new[] { "Choice_Id" });
            RenameColumn(table: "dbo.Votes", name: "Choice_Id", newName: "ChoiceId");
            AlterColumn("dbo.Choices", "ChoiceText", c => c.String(maxLength: 100));
            AlterColumn("dbo.Polls", "QuestionText", c => c.String(maxLength: 100));
            AlterColumn("dbo.Votes", "ChoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "ChoiceId");
            AddForeignKey("dbo.Votes", "ChoiceId", "dbo.Choices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "ChoiceId", "dbo.Choices");
            DropIndex("dbo.Votes", new[] { "ChoiceId" });
            AlterColumn("dbo.Votes", "ChoiceId", c => c.Int());
            AlterColumn("dbo.Polls", "QuestionText", c => c.String());
            AlterColumn("dbo.Choices", "ChoiceText", c => c.String());
            RenameColumn(table: "dbo.Votes", name: "ChoiceId", newName: "Choice_Id");
            CreateIndex("dbo.Votes", "Choice_Id");
            AddForeignKey("dbo.Votes", "Choice_Id", "dbo.Choices", "Id");
        }
    }
}
