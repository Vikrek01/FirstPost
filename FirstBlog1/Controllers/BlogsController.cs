using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstBlog1.Models;

namespace FirstBlog1.Controllers
{
    [Authorize(Roles = "User")]
    public class BlogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blogs
        public ActionResult Index()
        {
            Guid Id1 = (Guid)Session["Id"];
            var userData = (from user in db.Blogs
                            where user.Store == Id1
                            select user).ToList();

            return View(userData);
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Blog blog)
        {
              string filename = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                string extension = Path.GetExtension(file.FileName);


                string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                blog.Image = "~/Content/Images/" + _filename;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (file.ContentLength < 100000)
                    {
                        blog.Store = (Guid)Session["Id"];
                        db.Blogs.Add(blog);
                        db.SaveChanges();

                        file.SaveAs(path);
                        ViewBag.msg = "Student Added";
                        ModelState.Clear();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.msg = "File Size should be Less than 1 Mb";
                    }
                }
                else
                {
                    ViewBag.msg = "Invalid File Type";
                }
                return View();

        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

       
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Image,Heading,Description,Author,Store")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
