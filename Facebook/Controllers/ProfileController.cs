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
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            string userId = MethodAndFanction.getUserId();
            var Result = db.Users.SingleOrDefault(i => i.Id == userId);
            return View(Result);
        }
        public ActionResult InfoUse()
        {
            string userId = MethodAndFanction.getUserId();
            var Result = db.Users.SingleOrDefault(i => i.Id == userId);
            return PartialView(Result);
        }


        public ActionResult ListPostUser()
        {
            string userId = MethodAndFanction.getUserId();
            var Result = db.Users.SingleOrDefault(i => i.Id == userId).Post.OrderByDescending(i=>i.postDate);

            return PartialView(Result);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            string userId = MethodAndFanction.getUserId();
            var Result = db.Users.SingleOrDefault(i => i.Id == userId);
            return PartialView(Result);
        }
        [HttpPost]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                string userId = MethodAndFanction.getUserId();
                var Result = db.Users.SingleOrDefault(i => i.Id == userId);
                Result.userAddress = applicationUser.userAddress;
                //Result.UserName = applicationUser.UserName;
                Result.PhoneNumber = applicationUser.PhoneNumber;
                Result.userBhtDay = applicationUser.userBhtDay;
               // Result.Email = applicationUser.Email;
                db.SaveChanges();
                return RedirectToAction("InfoUse");
            }else
            return RedirectToAction("InfoUse");
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
