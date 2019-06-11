using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;

namespace Facebook.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult FriendsRequests()
        {

            string user_id= Session["ID"].ToString();

            var returnedList = 
                db.Friend.Where(f => f.RequestedToId.Equals(user_id)&&f.FriendRequestFlag==(FriendRequestFlag.Pending))
                .Select(f => f.RequestedBy)
                .ToList();
            
            return View(returnedList) ;
        }
        [HttpPost]
        public ActionResult AcceptRequest(string requestedID)
        {
            try { 
            var user_id = Session["ID"].ToString();
            var friend= db.Friend.Where(f => f.RequestedToId == user_id && f.RequestedById == requestedID).FirstOrDefault();
            friend.FriendRequestFlag = FriendRequestFlag.Approved;
            db.SaveChanges();
            return RedirectToAction("FriendsRequests");
            }catch(Exception)
            {
                return RedirectToAction("Index","Home");
            }
        }







        public bool SendFriendRequest(string recieverId)
        {
            var sender= Session["ID"].ToString();
            var friendRequest = new Friend() {
                BecameFriendsTime = DateTime.Now,
                FriendRequestFlag = FriendRequestFlag.None,
                RequestedById = sender,
                RequestedToId = recieverId
            };
            try
            {
                db.Friend.Add(friendRequest);
                db.SaveChanges();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
    }
}
