using FptBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FptBook.Controllers
{
    public class AccountController : Controller
    {
        private ModelDatabase _db = new ModelDatabase();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(account _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.accounts.FirstOrDefault(s => s.username == _user.username);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.accounts.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.ErrorMessage = "User name and Password duplicate";
                    return View();
                }
            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var _Password = GetMD5(password);
                var data = _db.accounts.Where(s => s.username.Equals(username) && s.password.Equals(_Password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().state == 0)
                    {
                        Session["UserName"] = data.FirstOrDefault().username;
                        Session["Phone"] = data.FirstOrDefault().phone;
                        Session["Address"] = data.FirstOrDefault().address;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //add session
                        Session["UserNameAdmin"] = data.FirstOrDefault().username;
                        return RedirectToAction("Index", "Books");
                    }

                }
                else
                {
                    //ViewBag.Error = "User name and Password wrong";
                    ViewBag.ErrorMessage = "User name and Password wrong";
                    //return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Editinfor()
        {
            var UserName = Session["UserName"];
            account obj = _db.accounts.ToList().Find(x => x.username.Equals(UserName));
            if (obj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editinfor(account _user)
        {
            if (ModelState.IsValid)
            {
                _db.accounts.Attach(_user);

                _db.Entry(_user).Property(a => a.fullname).IsModified = true;
                _db.Entry(_user).Property(a => a.email).IsModified = true;
                _db.Entry(_user).Property(a => a.phone).IsModified = true;
                _db.Entry(_user).Property(a => a.address).IsModified = true;

                _db.SaveChanges();

                Response.Write("<script>alert('Update information success!');window.location='/';</script>");
            }
            return View(_user);
        }

        public ActionResult ChangePass()
        {
            var user = Session["UserName"];
            if (user == null)
            {
                Response.Write("<script>alert('Please sign in to continue!'); window.location='/Account/SignIn'</script>");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(account _user)
        {
            var user = Session["Username"];

            account objAccount = _db.accounts.ToList().Find(p => p.username.Equals(user) && p.password.Equals(GetMD5(_user.CurrentPassword)));
            if (objAccount == null)
            {
                ViewBag.Error = "Current Password is incorrect";
                return View();
            }
            if (_user.NewPassword != _user.ConfirmNewPassword)
            {
                ViewBag.Confirm = "The new password and confirmation new password do not match.";
            }

            else
            {
                objAccount.password = GetMD5(_user.NewPassword);

                objAccount.ConfirmPassword = objAccount.password;
                _db.accounts.Attach(objAccount);
                _db.Entry(objAccount).Property(a => a.password).IsModified = true;
                _db.SaveChanges();

                ViewBag.Success = "Password Change successfully";
            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}