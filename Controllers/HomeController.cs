using FptBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FptBook.Controllers
{
    public class HomeController : Controller
    {
        private ModelDatabase _db = new ModelDatabase();
        // GET: Home
        public ActionResult Index()
        {
            var DataBook = _db.books.ToList();
            return View(DataBook);
        }
        public ActionResult Cart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string Search)
        {
            ViewBag.Search = Search;
            var book = _db.books.ToList().Where(s => s.bookName.ToUpper().Contains(Search.ToUpper()) ||
                 s.author.authorName.ToUpper().Contains(Search.ToUpper()) ||
                 s.category.categoryName.ToUpper().Contains(Search.ToUpper()));

            return View(book);

        }
    }
}
