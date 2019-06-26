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
    public class PostImageController : Controller
    {
        private AppleCatEntities2 db = new AppleCatEntities2();

        //
        // GET: /PostImage/

        public ActionResult Index()
        {
            var postimages = db.PostImages.Include(p => p.Post);
            return View(postimages.ToList());
        }

        //
        // GET: /PostImage/Details/5

        public ActionResult Details(int id = 0)
        {
            PostImage postimage = db.PostImages.Find(id);
            if (postimage == null)
            {
                return HttpNotFound();
            }
            return View(postimage);
        }

        //
        // GET: /PostImage/Create

        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title");
            return View();
        }

        //
        // POST: /PostImage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostImage postimage)
        {
            if (ModelState.IsValid)
            {
                db.PostImages.Add(postimage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", postimage.PostID);
            return View(postimage);
        }

        //
        // GET: /PostImage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PostImage postimage = db.PostImages.Find(id);
            if (postimage == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", postimage.PostID);
            return View(postimage);
        }

        //
        // POST: /PostImage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostImage postimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postimage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.Posts, "PostID", "Title", postimage.PostID);
            return View(postimage);
        }

        //
        // GET: /PostImage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PostImage postimage = db.PostImages.Find(id);
            if (postimage == null)
            {
                return HttpNotFound();
            }
            return View(postimage);
        }

        //
        // POST: /PostImage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostImage postimage = db.PostImages.Find(id);
            db.PostImages.Remove(postimage);
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