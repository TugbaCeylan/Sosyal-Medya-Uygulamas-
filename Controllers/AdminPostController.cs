using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcElmaKedisi.Models;

namespace MvcElmaKedisi.Controllers
{
    [AdminAuthorize]
    public class AdminPostController : Controller
    {
        private AppleCatEntities2 db = new AppleCatEntities2();

        //
        // GET: /AdminPost/

        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User);
            return View(posts.ToList());
        }

        //
        // GET: /AdminPost/Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /AdminPost/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            ViewBag.CategooryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        //
        // POST: /AdminPost/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategooryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", post.UserID);
            return View(post);
        }

        //
        // GET: /AdminPost/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategooryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", post.UserID);
            return View(post);
        }

        //
        // POST: /AdminPost/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategooryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", post.UserID);
            return View(post);
        }

        //
        // GET: /AdminPost/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /AdminPost/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Entry(db.Posts.Find(id)).CurrentValues.SetValues(db.Posts.Find(id).IsDeleted = true);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}