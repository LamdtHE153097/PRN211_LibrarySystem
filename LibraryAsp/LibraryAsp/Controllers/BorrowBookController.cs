using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class BorrowBookController : Controller
    {

        BookDao bookDao = new BookDao();
        PublisherDao publisherDao = new PublisherDao();
        CategoryDao categoryDao = new CategoryDao();
        TransactionDao transactionDao = new TransactionDao();
        AuthenticationDao authenticationDao = new AuthenticationDao();

        // GET: BorrowBook
        public ActionResult Index(string mess)
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 1)
            {
                ViewBag.listP = publisherDao.getAll();
                ViewBag.listC = categoryDao.getAll();
                ViewBag.list = bookDao.getAll();
                ViewBag.mes = mess;
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
            Transaction transaction = new Transaction();
            transaction.id_user = Int32.Parse(form["idUser"]);
            transaction.id_book = Int32.Parse(form["idBook"]);
            transaction.start_time = DateTime.Parse(form["start_time"]);
            transaction.end_time = DateTime.Parse(form["end_time"]);
            transaction.createdAt = DateTime.Now;
            transaction.status = 1;
            Transaction obj = transactionDao.checkExistTransaction(Int32.Parse(form["idUser"]), Int32.Parse(form["idBook"]));
            if(obj == null)
            {
                transactionDao.borrowBook(transaction);
                return RedirectToAction("Index", new { mess = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { mess = "2" });
            }
           
        }

        public ActionResult ListTransactionBorrow()
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 1)
            {
                //var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
                var id = userInfomatiom.id_user;
                ViewBag.listUser = authenticationDao.getAll();
                ViewBag.listBook = bookDao.getAll();
                ViewBag.list = transactionDao.getTransactionBorrow(id);
                return View();
            } else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ListTransactionPunish()
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 1)
            {
                var id = userInfomatiom.id_user;
                ViewBag.listUser = authenticationDao.getAll();
                ViewBag.listBook = bookDao.getAll();
                ViewBag.list = transactionDao.getTransactionPunish(id);
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ListTransaction(string mess)
        {
            ViewBag.mes = mess;
            ViewBag.listUser = authenticationDao.getAll();
            ViewBag.listBook = bookDao.getAll();
            ViewBag.list = transactionDao.getTransaction();
            return View();
        }
//1: Chờ duyệt 
//2: Đang thuê 
//3: Đã trả
//4: Bị phạt
        public ActionResult changeStatus(int id, int status)
        {
            transactionDao.updateStatus(status, id);
            var transaction = transactionDao.getTransaction(id);
            if(status == 2)
            {
                Book book = bookDao.getOneBook(transaction.id_book);
                bookDao.editQuantity(book.id_book, book.quantity - 1);
            }
            return RedirectToAction("ListTransaction", new { mess = "1" });
        }
    }
}