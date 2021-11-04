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
            ViewBag.listP = publisherDao.getAll();
            ViewBag.listC = categoryDao.getAll();
            ViewBag.list = bookDao.getAll();
            ViewBag.mes = mess; 
            return View();
        }

        [HttpPost]
        public ActionResult add(FormCollection form)
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
            var id = userInfomatiom.id_user;
            ViewBag.listUser = authenticationDao.getAll();
            ViewBag.listBook = bookDao.getAll();
            ViewBag.list = transactionDao.getTransactionBorrow(id);
            return View();
        }

        public ActionResult ListTransactionPunish()
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            var id = userInfomatiom.id_user;
            ViewBag.listUser = authenticationDao.getAll();
            ViewBag.listBook = bookDao.getAll();
            ViewBag.list = transactionDao.getTransactionPunish(id);
            return View();
        }

        public ActionResult ListTransaction(string mess)
        {
            ViewBag.mes = mess;
            ViewBag.listUser = authenticationDao.getAll();
            ViewBag.listBook = bookDao.getAll();
            ViewBag.list = transactionDao.getTransaction();
            return View();
        }

        public ActionResult changeStatus(int id, int status)
        {
            transactionDao.updateStatus(status, id);
            return RedirectToAction("ListTransaction", new { mess = "1" });
        }
    }
}