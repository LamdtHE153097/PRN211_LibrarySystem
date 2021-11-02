using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        AuthenticationDao authenticationDao = new AuthenticationDao();

        public ActionResult Index()
        {
            User user = (User)Session["USER"];
            if(user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                var list = authenticationDao.getInformationByUserName(user.email);
                ViewBag.list = list;
                return View();
            }
           
        }
        public ActionResult Edit(string mess)
        {
            User user = (User)Session["USER"];
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                var list = authenticationDao.getInformationByUserName(user.email);
                ViewBag.list = list;
                ViewBag.mes = mess;
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditPost(FormCollection form)
        {
            User user = new User();
            user.fullname = form["fullname"];
            user.email = form["email"];
            user.gender = Int32.Parse(form["gender"]);
            user.phone = form["phone"];
            user.address = form["address"];
            user.birthday = DateTime.Parse(form["birthday"]);
            authenticationDao.editUser(user);
            return RedirectToAction("Edit", new { mess = "1" });
        }

        public ActionResult UpdatePassword(string mess)
        {
            User user = (User)Session["USER"];
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                ViewBag.mes = mess;
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdatePasswordPost(FormCollection form)
        {
            User user = (User)Session["USER"];
            var password = form["password"];
            var rePassword = form["rePassword"];
            if (password.Equals(rePassword))
            {
                authenticationDao.updatePassword(user.email,password);
                return RedirectToAction("UpdatePassword", new { mess = "1" });
            }
            else
            {
                return RedirectToAction("UpdatePassword", new { mess = "2" });
            }
        }
        public ActionResult ListUser(string mess)
        {
            ViewBag.mes = mess;
            ViewBag.list = authenticationDao.getStudent();
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(FormCollection form)
        {
            User user = new User();
            user.fullname = form["fullname"];
            user.email = form["email"];
            user.gender = Int32.Parse(form["gender"]);
            user.phone = form["phone"];
            user.address = form["address"];
            user.birthday = DateTime.Parse(form["birthday"]);
            user.password = form["matkhau"];
            user.id_role = 1;
            authenticationDao.addUser(user);
            return RedirectToAction("ListUser", new { mess = "1" });
        }
    }
}