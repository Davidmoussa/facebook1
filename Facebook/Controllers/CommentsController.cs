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

    
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // Create Comment  ??
        // get
       public ActionResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddComment( Comment comment )
        {
            //var Result = db.Comments.Where(i => i.postId == postId && i.commentDeleted == false).OrderBy(i => i.commentDate).Include(i => i.ApplicationUser).ToList();
            int postId = int.Parse(TempData["PostId"].ToString());
            if ( Session["ID"]!=null && postId >0)
            {
                //int postId = int.Parse(TempData["PostId"].ToString());
                comment.postId = postId;
                comment.userId = Session["ID"].ToString();
                comment.commentDate = DateTime.Now;
                comment.commentDeleted = false;

                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("ListComment",new{ postId= postId });
            }
            return RedirectToAction("ListComment", new { postId = postId });
        }


        public ActionResult ListComment(int postId=-1)
        {
            if (postId > 0) {
                TempData["PostId"] = postId;
                var Result = db.Comments.Where(i => i.postId == postId && i.commentDeleted == false).OrderBy(i => i.commentDate).Include(i => i.ApplicationUser).ToList();
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
