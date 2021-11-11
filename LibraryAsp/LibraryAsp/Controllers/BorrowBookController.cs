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
        LibraryDbContext context = new LibraryDbContext();
        // GET: BorrowBook
        public ActionResult Index(string mess)
        {
            var query = context.categories.Select(c => new { c.id_category, c.name });
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 1)
            {
                ViewBag.listP = publisherDao.getAll();
                ViewBag.listC = categoryDao.getAll();
                ViewBag.list = bookDao.getAll();
                ViewBag.listTest = new SelectList(query.AsEnumerable(), "id_category", "name");
                ViewBag.mes = mess;
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
         }

        [HttpPost]
        public ActionResult Index (Category category)
        {
            if (category.id_category != 0)
            {
                var query = context.categories.Select(c => new { c.id_category, c.name });
                ViewBag.listP = publisherDao.getAll();
                ViewBag.listC = categoryDao.getAll();
                ViewBag.list = context.books.Where(p => p.id_category == category.id_category).ToList();
                ViewBag.listTest = new SelectList(query.AsEnumerable(), "id_category", "name");
                return View();
            } else
            {
                var query = context.categories.Select(c => new { c.id_category, c.name });
                ViewBag.listP = publisherDao.getAll();
                ViewBag.listC = categoryDao.getAll();
                ViewBag.list = bookDao.getAll();
                ViewBag.listTest = new SelectList(query.AsEnumerable(), "id_category", "name");
                return View();
                //string mess = "1";
                //return RedirectToAction("Index", new { mess });
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
            DateTime dateNow = DateTime.Now;
            List<Transaction> transactions = transactionDao.getTransaction();
            foreach(var item in transactions)
            {
                TimeSpan ts = item.end_time - item.start_time;
                int differenceInDays = ts.Days;
                TimeSpan tsNow = dateNow - item.start_time;
                if((tsNow.Days >= differenceInDays) && item.status == 2)
                {
                    // update item to punish
                    transactionDao.autoPunish(item.id_transaction);
                }
            }
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 2)
            {
                ViewBag.mes = mess;
                ViewBag.listUser = authenticationDao.getAll();
                ViewBag.listBook = bookDao.getAll();
                ViewBag.list = transactionDao.getTransaction();
                return View();
            } else
            {
                return RedirectToAction("Error", "Home");
            }
                
        }
//1: Chờ duyệt 
//2: Đang thuê 
//3: Đã trả
//4: Bị phạt
        public ActionResult changeStatus(int id, int status)
        {
            transactionDao.updateStatus(status, id);
            var transaction = transactionDao.getTransaction(id);
            Book book = bookDao.getOneBook(transaction.id_book);
            if (status == 2)
            {
                bookDao.editQuantity(book.id_book, book.quantity - 1);
            } else if(status == 3)
            {
                bookDao.editQuantity(book.id_book, book.quantity + 1);
            }

            return RedirectToAction("ListTransaction", new { mess = "1" });
        }
    }
}