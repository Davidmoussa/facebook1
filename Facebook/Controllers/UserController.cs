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
            Session["ID"] = MethodAndFanction.getUserId().ToString();
            string user_id= Session["ID"].ToString();

            var returnedList = 
                db.Friend.Where(f => f.RequestedToId.Equals(user_id)&&f.FriendRequestFlag==(FriendRequestFlag.None))
                .Select(f => f.RequestedBy)
                .ToList();
            
            return PartialView(returnedList) ;
        }
        [HttpPost]
        public ActionResult AcceptRequest(string requestedID)
        {
            try {
                Session["ID"] = MethodAndFanction.getUserId().ToString();
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

        [HttpGet]
        public ActionResult Index()
        {
            Session["ID"] = MethodAndFanction.getUserId().ToString();
           // string user_id = Session["ID"].ToString();

            string userID = Session["ID"].ToString();
            var Result = db.Users.SingleOrDefault(i => i.Id == userID).Friends.ToList(); 
            return View(Result); 
        }
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView();

        }
        [HttpPost]
        public ActionResult Search(string keyUsername)
        {
            if (keyUsername != null)
            {
                string userID = Session["ID"].ToString();
                var result = db.Users.Where(i => i.UserName.Contains(keyUsername) ).ToList();
               ModelState.Clear();
                if (result != null)
                {
                    var Sent = db.Users.SingleOrDefault(i => i.Id == userID).SentFriendRequests.Where(j => j.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedTo).ToList();
                    var Receieved = db.Users.SingleOrDefault(i => i.Id == userID).ReceievedFriendRequests.Where(h => h.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedBy).ToList();
                    var x = result.Except(Sent);
                    x = x.Except(Receieved);
                    //return RedirectToAction("DisplayAll" ,x.ToList());
                    return PartialView("DisplayAll", x.ToList());
                }


            }
           
                return RedirectToAction("Index");
           

                  
        }

        public ActionResult DisplayAll(ICollection<ApplicationUser> list)
        {

            if (list != null)
            {
                return PartialView(list.ToList());
            }
            else
            {
                string userID = MethodAndFanction.getUserId().ToString();
                var result = db.Users.ToList();
                var Sent = db.Users.SingleOrDefault(i => i.Id == userID).SentFriendRequests.Where(j => j.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedTo).ToList();
                var Receieved = db.Users.SingleOrDefault(i => i.Id == userID).ReceievedFriendRequests.Where(h => h.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedBy).ToList();
                var x = result.Except(Sent);
                x = x.Except(Receieved);
                 return  PartialView(x);
            }
           

        }

        public ActionResult SendFriendRequest(string recieverId)
        {

            var sender = MethodAndFanction.getUserId().ToString();
           
           
            try
            {
                new Friend().AddFriendRequest(db.Users.SingleOrDefault(i => i.Id == sender), db.Users.SingleOrDefault(i => i.Id == recieverId));
                string userID = MethodAndFanction.getUserId().ToString();
                var result = db.Users.ToList();
                var Sent = db.Users.SingleOrDefault(i => i.Id == userID).SentFriendRequests.Where(j => j.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedTo).ToList();
                var Receieved = db.Users.SingleOrDefault(i => i.Id == userID).ReceievedFriendRequests.Where(h => h.FriendRequestFlag == FriendRequestFlag.Approved).Select(i => i.RequestedBy).ToList();
                var x = result.Except(Sent);
                x = x.Except(Receieved);

                db.SaveChanges();
                return RedirectToAction("DisplayAll", x); ;
            }catch(Exception)
            {

                return RedirectToAction("DisplayAll");
            }
        }
    }
}
