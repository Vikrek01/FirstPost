using FirstBlog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FirstBlog1.Controllers
{
    public class CredentialsController : Controller
    {
        // GET: Credential

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == model.UserName);

            if (user != null)
            {
                if (user.Password == model.UserPassword)
                {
                    if (user.Role == "Admin")
                    {
                       
                        Session["username"] = model.UserName;
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role == "User")
                    {
                        Session["ID"] = user.UserId;
                        Session["username"] = model.UserName;
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Blogs");
                    }

                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }


        }

       


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Login");
        }





        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                User user1 = db.Users.FirstOrDefault(emp => emp.UserName == user.UserName);

                if (user1 == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Credentials");
                }
                else
                {
                    ModelState.AddModelError("", "Already exist");
                }
            }

            return View(user);
        }


    }
}