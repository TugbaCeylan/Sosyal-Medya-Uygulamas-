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
    public class UserController : Controller
    {
        private AppleCatEntities2 db = new AppleCatEntities2();


        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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