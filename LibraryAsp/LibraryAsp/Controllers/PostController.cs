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
            ViewBag.Msg = msg;
            BookDao book = new BookDao();
            ViewBag.List = post.getAll();
            return View();
        }
        //[HttpPost]
        //public ActionResult Add(FormCollection form)
        //{
        //    Post p = new Post();
        //    p.title = form[""];
        //    post.add(pub);
        //    return RedirectToAction("Index", new { msg = "1" });
        //}
        //[HttpPost]
        //public ActionResult Update(FormCollection form)
        //{
        //    Publisher pub = new Publisher();
        //    pub.id_publisher = Int32.Parse(form["id_publisher"]);
        //    pub.name = form["name"];
        //    publisher.edit(pub);
        //    return RedirectToAction("Index", new { msg = "1" });
        //}

        //[HttpPost]
        //public ActionResult Delete(FormCollection form)
        //{
        //    Publisher pub = new Publisher();
        //    pub.id_publisher = Convert.ToInt32(form["id"]);
        //    publisher.delete(pub.id_publisher);
        //    return RedirectToAction("Index", new { msg = "1" });
        //}
    }
}