using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FptBook.Models;

namespace FptBook.Controllers
{
    public class AccountsController : Controller
    {
        private ModelDatabase db = new ModelDatabase();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.accounts.ToList());
        }

        // GET: Accounts/Details
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Edit
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(account account)
        {
            if (ModelState.IsValid)
            {
                db.accounts.Attach(account);

                db.Entry(account).Property(a => a.fullname).IsModified = true;
                db.Entry(account).Property(a => a.email).IsModified = true;
                db.Entry(account).Property(a => a.phone).IsModified = true;
                db.Entry(account).Property(a => a.address).IsModified = true;
                db.Entry(account).Property(a => a.state).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
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
