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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
       

        // GET: Posts
        public ActionResult Index()
        {
            Session["ID"] = MethodAndFanction.getUserId().ToString();
            //            var posts = db.Posts.Include(p => p.ApplicationUser);
            return View();
        }

        public ActionResult Create()
        {
            
            return PartialView(new Post());
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postId,postContent,postDate,postDelete,userId")] Post post ,HttpPostedFileBase ImagePost)
        {
            string Userid = MethodAndFanction.getUserId().ToString();
            var frind = db.Users.SingleOrDefault(i => i.Id == Userid).Friends.Select(i => i.RequestedTo.Id).ToList<string>();
            frind.AddRange(db.Users.SingleOrDefault(i => i.Id == Userid).Friends.Select(i => i.RequestedTo.Id));
            frind.Add(Userid);
            var x = frind.Select(i => i).ToList<string>();
            var result = db.Posts.Where(i => x.Contains((string)i.userId) && i.ApplicationUser.userBlock == false).OrderByDescending(i => i.postDate).ToList();
            // var result = db.Posts.Where(i => i.postDelete == false).Include(i=>i.Like).Include(i=>i.ApplicationUser).Include(i=>i.Comment).OrderByDescending(i => i.postDate);
            ViewBag.userId = Userid; // id this user Create Post
            post.userId = Userid;   // id this user Create Post
            post.postDate = DateTime.Now;    //Time Create this post 
            post.postDelete = false;   // Default  is Not Deleted 
            if (ImagePost != null)    // if post  Has image
            {
                post.postImg = new byte[ImagePost.ContentLength]; //add Img in  Database
                ImagePost.InputStream.Read(post.postImg, 0, ImagePost.ContentLength);
            }
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                ModelState.Clear();
                return PartialView("displayAll",result.ToList());
            }

          
            return PartialView("displayAll" , result.ToList());
        }

        public ActionResult displayAll()
        {
            string Userid = MethodAndFanction.getUserId().ToString();
          // var result = db.Posts.Where(i => i.postDelete == false ).Include(i => i.Like).Include(i => i.ApplicationUser).Include(i => i.Comment).OrderByDescending(i => i.postDate);
            var frind = db.Users.SingleOrDefault(i => i.Id == Userid).Friends.Select(i => i.RequestedTo.Id).ToList<string>();
            frind.AddRange(db.Users.SingleOrDefault(i => i.Id == Userid).Friends.Select(i => i.RequestedTo.Id));
            frind.Add(Userid);
            var x = frind.Select(i => i).ToList<string>();
            var result = db.Posts.Where(i => x.Contains((string)i.userId )&&i.ApplicationUser.userBlock==false).OrderByDescending(i=>i.postDate).ToList();
            return PartialView(result.ToList());
        }
        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            TempData["PostIdEdit"] = id;
            return PartialView(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Post post)
        {
            if (ModelState.IsValid)
            {
              int idPost =int.Parse(TempData["PostIdEdit"].ToString());
                db.Posts.SingleOrDefault(i => i.postId == idPost).postContent = post.postContent;
                db.SaveChanges();
                return RedirectToAction("ListPostUser", "Profile");
            }
           
            return PartialView(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
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
