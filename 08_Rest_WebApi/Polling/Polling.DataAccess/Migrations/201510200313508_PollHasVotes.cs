namespace Polling.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollHasVotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "PollId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "PollId");
            AddForeignKey("dbo.Votes", "PollId", "dbo.Polls", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "PollId", "dbo.Polls");
            DropIndex("dbo.Votes", new[] { "PollId" });
            DropColumn("dbo.Votes", "PollId");
        }
    }
}
