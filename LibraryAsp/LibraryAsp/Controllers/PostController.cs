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
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = post.getAll();
            return View();
        }
    }
}