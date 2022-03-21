using FptBook.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace FptBook.Controllers
{
    public class BooksController : Controller
    {
        private ModelDatabase db = new ModelDatabase();

        // GET: Books
        public ActionResult Index()
        {
            if (Session["UserNameAdmin"] != null)
            {
                var books = db.books.Include(b => b.author).Include(b => b.category);
                return View(books.ToList());
                //return View();
            }
            //return View("Error");
            return RedirectToAction("Error", "Admin");

        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName");
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName");
            return View();
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image, book Book)
        {
            if (ModelState.IsValid)
            {
                var check = db.books.FirstOrDefault(x => x.bookName.Equals(Book.bookName));
                if (check == null && image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/assets/img/Mangas"), pic);
                    string checkimg = Path.GetExtension(image.FileName);
                    if (checkimg.ToLower() == ".jpg" || checkimg.ToLower() == ".jpeg" || checkimg.ToLower() == ".png")
                    {
                        image.SaveAs(path);
                        Book.image = pic;
                        db.books.Add(Book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.CheckError = "Invavlid file";
                    }

                }
                else
                {
                    ViewBag.Error = "This Book is already exist";
                    return View();
                }
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }


        // GET: Books/Edit/
        public ActionResult Edit(string id)
        {
            if (Session["UserNameAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book Book = db.books.Find(id);
                Session["imgPath"] = "~/assets/img/Mangas/" + Book.image;
                if (Book == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AuthorID = new SelectList(db.authors, "AuthorID", "AuthorName", Book.authorID);
                ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName", Book.categoryID);
                return View(Book);
            }
            return View("Error");
        }

        // POST: Books/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase image, book Book)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/assets/img/Mangas/"), pic);
                    string oldPath = Request.MapPath(Session["imgPath"].ToString());
                    string checkimg = Path.GetExtension(image.FileName);
                    if (checkimg.ToLower() == ".jpg" || checkimg.ToLower() == ".jpeg" || checkimg.ToLower() == ".png")
                    {
                        image.SaveAs(path);

                        Book.image = pic;

                        db.Entry(Book).State = EntityState.Modified;
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.CheckError = "Invavlid file";
                    }
                }
                else
                {
                    db.books.Attach(Book);

                    db.Entry(Book).Property(a => a.price).IsModified = true;
                    db.Entry(Book).Property(a => a.quantity).IsModified = true;
                    db.Entry(Book).Property(a => a.authorID).IsModified = true;
                    db.Entry(Book).Property(a => a.categoryID).IsModified = true;
                    db.Entry(Book).Property(a => a.bookName).IsModified = true;
                    db.Entry(Book).Property(a => a.shortDesc).IsModified = true;
                    db.Entry(Book).Property(a => a.detailDesc).IsModified = true;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }

        // GET: Books/Delete/
        public ActionResult Delete(string id)
        {
            if (Session["UserNameAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book Book = db.books.Find(id);
                Session["imgOldPath"] = "~/assets/img/Mangas/" + Book.image;
                if (Book == null)
                {
                    return HttpNotFound();
                }
                return View(Book);
            }
            return View("Error");
        }

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string oldPath = Request.MapPath(Session["imgOldPath"].ToString());
            book Book = db.books.Find(id);
            db.books.Remove(Book);
            System.IO.File.Delete(oldPath);
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
