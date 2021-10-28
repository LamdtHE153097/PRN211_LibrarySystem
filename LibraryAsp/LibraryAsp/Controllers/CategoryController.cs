using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDao category = new CategoryDao();
        // GET: Category
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = category.getAll();
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            Category pub = new Category();
            pub.name = form["name"];
            category.add(pub);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}