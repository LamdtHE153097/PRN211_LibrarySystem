using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class PostController : Controller
    {
        PostDao post = new PostDao();
        // GET: Post
        public ActionResult Index(int id)
        {
            ViewBag.Post = post.getInformationById(Convert.ToInt32(id));
            BookDao book = new BookDao();
            ViewBag.List = post.getAll();
            ViewBag.Book = book.getFiveBook();
            return View();
        }
        public ActionResult getPostById(int id)
        {
            return RedirectToAction("Index", new { id });
        }
        public ActionResult Manage(string msg)
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 2)
            {
                ViewBag.Msg = msg;
                BookDao book = new BookDao();
                ViewBag.List = post.getAll();
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            var title = form["title"];
            var publisher = Int32.Parse(form["publisher"]);
            var content = form["content"];
            DateTime createdAt = DateTime.Now;
            post.add(title, content, publisher, createdAt, 1);
            return RedirectToAction("Manage", new { msg = "1" });
        }
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            var title = form["title"];
            var id_publisher = Int32.Parse(form["publisher"]);
            var content = form["content"];
            var id_post = Int32.Parse(form["id_post"]);

            post.update(title, content, id_publisher, id_post);
            return RedirectToAction("Manage", new { msg = "1" });
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            Post postDel = new Post();
            postDel.id_post = Convert.ToInt32(form["id_post"]);
            post.delete(postDel.id_post);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}