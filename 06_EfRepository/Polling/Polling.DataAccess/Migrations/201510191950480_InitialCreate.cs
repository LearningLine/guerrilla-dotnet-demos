namespace Polling.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        ChoiceText = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Choice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Choices", t => t.Choice_Id)
                .Index(t => t.Choice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Choice_Id", "dbo.Choices");
            DropForeignKey("dbo.Choices", "PollId", "dbo.Polls");
            DropIndex("dbo.Votes", new[] { "Choice_Id" });
            DropIndex("dbo.Choices", new[] { "PollId" });
            DropTable("dbo.Votes");
            DropTable("dbo.Polls");
            DropTable("dbo.Choices");
        }
    }
}
