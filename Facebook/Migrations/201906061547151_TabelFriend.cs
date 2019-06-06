namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelFriend : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "postId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "postid", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        RequestedById = c.String(nullable: false, maxLength: 128),
                        RequestedToId = c.String(nullable: false, maxLength: 128),
                        FriendRequestFlag = c.Int(nullable: false),
                        BecameFriendsTime = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.RequestedById, t.RequestedToId })
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedById)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestedToId)
                .Index(t => t.RequestedById)
                .Index(t => t.RequestedToId);
            
            AddForeignKey("dbo.Comments", "userId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "postId", "dbo.Posts", "postId");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "userID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "postid", "dbo.Posts", "postId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Likes", "postid", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "postId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "RequestedById", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "RequestedToId" });
            DropIndex("dbo.Friends", new[] { "RequestedById" });
            DropTable("dbo.Friends");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "postid", "dbo.Posts", "postId", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "userID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "postId", "dbo.Posts", "postId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "userId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
