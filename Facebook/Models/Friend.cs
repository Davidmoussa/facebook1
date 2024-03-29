﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Facebook.Models
{
    public class Friend
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        [Column(Order = 0)]
        //[ForeignKey("RequestedBy")]
        public string RequestedById { get; set; }

        [Key]
        [Column(Order = 1)]
        //[ForeignKey("RequestedTo")]
        public string RequestedToId { get; set; }

        public FriendRequestFlag FriendRequestFlag { get; set; }

        //Time When   FriendRequestFlag== Approved
        public DateTime? BecameFriendsTime { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual ApplicationUser RequestedTo { get; set; }


       
        public void AddFriendRequest(ApplicationUser user, ApplicationUser friendUser)
        {
            if (user.Id != friendUser.Id)
            {
                var friendRequest = new Friend()
                {
                    RequestedBy = user,
                    RequestedTo = friendUser,

                    FriendRequestFlag = FriendRequestFlag.None
                };
                user.SentFriendRequests.Add(friendRequest);
            }
           
        }

    }
    public enum FriendRequestFlag
    {
        None,
        Approved,
        Rejected,
        Blocked,
        Spam,
        Pending
    };
}