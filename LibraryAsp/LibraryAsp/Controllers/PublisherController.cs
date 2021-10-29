using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class PublisherController : Controller
    {
        PublisherDao publisher = new PublisherDao();
        // GET: Publisher
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = publisher.getAll();
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            Publisher pub = new Publisher();
            pub.name = form["name"];
            publisher.add(pub);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}