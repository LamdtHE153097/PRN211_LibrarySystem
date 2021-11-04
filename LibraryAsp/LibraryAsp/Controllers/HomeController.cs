using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = Session["USER"];
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                PostDao post = new PostDao();
                BookDao book = new BookDao();
                //ViewBag.Msg = msg;
                ViewBag.List = post.getAll();
                ViewBag.Book = book.getFiveBook();
                ViewBag.Latest = post.getLatestPost();
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}