using LibraryAsp.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            BookDao bookDao = new BookDao();
            AuthenticationDao authenticationDao = new AuthenticationDao();
            var listUser = authenticationDao.getAll();
            ViewBag.listUser = listUser;
            var listBook = bookDao.getAll();
            ViewBag.listBook = listBook;
            TransactionDao dao = new TransactionDao();
            var noti = dao.getFiveNoti();
            ViewBag.a = noti;
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }
    }
}