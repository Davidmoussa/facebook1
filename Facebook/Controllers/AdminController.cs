using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facebook.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            var Result = db.Users.ToList();
            return View();
        }


        public ActionResult Users()
        {
            var Result = db.Users.ToList();
            return PartialView(Result);
        }

        public ActionResult Edit(string  id)
        {
            if (id != null)
            {
                TempData["userId"] = id.ToString();
                var Result = db.Users.SingleOrDefault(i => i.Id == id);
                return PartialView(Result);
            }else
                return PartialView();
        }
        [HttpPost]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            
            string id = TempData["userId"].ToString();
            if (id != null)
            {
                var Result = db.Users.SingleOrDefault(i => i.Id == id);
                Result.userBlock = applicationUser.userBlock;
                db.SaveChanges();
                
                return RedirectToAction("Users");
            }
            else
                return RedirectToAction("Users");
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