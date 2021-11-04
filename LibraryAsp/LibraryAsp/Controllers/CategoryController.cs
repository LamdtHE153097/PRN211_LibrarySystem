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
        LibraryDbContext _context = new LibraryDbContext();
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

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            Category cat = new Category();
            cat.id_category = Convert.ToInt32(form["id_category"]);
            cat.name = form["name"];
            category.edit(cat);
            return RedirectToAction("Index", new { msg = "1" });
        }

        [HttpPost]
        public ActionResult DeleteCategory(FormCollection form)
        {
            Category cat = new Category();
            cat.id_category = Convert.ToInt32(form["id"]);
            category.delete(cat.id_category);
            return RedirectToAction("Index", new { msg="1"});
        }

    }
}