﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Facebook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {


        public DateTime userBhtDay { get; set; }

        public string userAddress { get; set; }
        public bool userBlock { get; set; }
        public byte[] userImage { get; set; }

        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }

        //anly  Frand Requests 
        public virtual ICollection<Friend> SentFriendRequests { get; set; }
        //anly  Frand ReceievedFriendRequests
        public virtual ICollection<Friend> ReceievedFriendRequests { get; set; }
        // get  All  Friend OF this  user only  Friend
        [NotMapped]
        public virtual ICollection<Friend> Friends
        {
            get
            {
                //Concat  this Friend Send And Receieved 
                var friends = SentFriendRequests.Where(i=>i.FriendRequestFlag==FriendRequestFlag.Approved).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(i => i.FriendRequestFlag == FriendRequestFlag.Approved));
                return friends;
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Friend>()
           .HasRequired(a => a.RequestedBy)
           .WithMany(b => b.SentFriendRequests)
           .HasForeignKey(c => c.RequestedById);

            modelBuilder.Entity<Friend>()
           .HasRequired(a => a.RequestedTo)
           .WithMany(b => b.ReceievedFriendRequests)
           .HasForeignKey(c => c.RequestedToId);

        }

        //  public IEnumerable ApplicationUsers { get; internal set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Friend> Friend { get; set; }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

       
    }
}