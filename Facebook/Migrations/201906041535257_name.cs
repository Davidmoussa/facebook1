namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "userImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "userImage");
        }
    }
}
