using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FptBook.Models;

namespace FptBook.Controllers
{
    public class AuthorsController : Controller
    {
        private ModelDatabase db = new ModelDatabase();

        // GET: Authors

        public ActionResult Index()
        {
            if (Session["UserNameAdmin"] != null)
            {
                return View(db.authors.ToList());
            }
            return RedirectToAction("Error");
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "authorID,authorName,description")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "authorID,authorName,description")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            author author = db.authors.Find(id);
            db.authors.Remove(author);
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
