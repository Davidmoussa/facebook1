namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friend3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Friends", new[] { "RequestedById" });
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.Friends", "RequestedById", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Friends", new[] { "RequestedById", "RequestedToId" });
            CreateIndex("dbo.Friends", "RequestedById");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Friends", new[] { "RequestedById" });
            DropPrimaryKey("dbo.Friends");
            AlterColumn("dbo.Friends", "RequestedById", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Friends", new[] { "RequestedById", "RequestedToId" });
            CreateIndex("dbo.Friends", "RequestedById");
        }
    }
}
