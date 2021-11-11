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

        public ActionResult Index(string mess)
        {
            User user = (User)Session["USER"];
            if(user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                var list = authenticationDao.getInformationByUserName(user.email);
                ViewBag.Msg = Convert.ToInt32(mess);
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
        public ActionResult EditUser(FormCollection form)
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

        [HttpPost]
        public ActionResult UpdatePassword(FormCollection form)
        {
            User user = (User)Session["USER"];
            var oldPassword = form["oldPassword"];
            var password = form["password"];
            var rePassword = form["rePassword"];
            if (password.Equals(rePassword) && (user.password.Equals(oldPassword)))
            {
                authenticationDao.updatePassword(user.email,password);
                return RedirectToAction("Index", new { mess = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { mess = "2" });
            }
        }
        public ActionResult ListUser(string mess)
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 2)
            {
                ViewBag.mes = mess;
                ViewBag.list = authenticationDao.getStudent();
                return View();
            } else
            {
                return RedirectToAction("Error", "Home");
            }
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