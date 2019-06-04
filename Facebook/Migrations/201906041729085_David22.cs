namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class David22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "postImg", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "postImg");
        }
    }
}
