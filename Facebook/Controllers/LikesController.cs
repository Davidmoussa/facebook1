using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;

namespace Facebook.Controllers
{
    public class LikesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       

        
        public ActionResult Create( int postId)
        {

            string userId = Session["ID"].ToString();
            if (userId != null)
            {
                var result = db.Like.SingleOrDefault(i => i.userID == userId && i.postid == postId);
                if (result == null)
                {
                    db.Like.Add(new Like() { userID = userId, postid = postId });
                    db.SaveChanges();

                }
                else
                {
                    db.Like.Remove(result);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("displayAll","Posts");
        }

       

       
        
        public ActionResult displayAll( int? postId)
        {
            if (postId != null)
            {
                var Result = db.Like.Where(i => i.postid == postId).Include(i => i.ApplicationUser).ToList();
                return PartialView(Result);
            }
            return null;
           
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
