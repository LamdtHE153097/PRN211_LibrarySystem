using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryAsp.Controllers
{
    public class BookController : Controller
    {
        BookDao book = new BookDao();
        // GET: Book
        public ActionResult Index(string msg)
        {
            var userInfomatiom = (LibraryAsp.Models.User)Session["USER"];
            if (userInfomatiom.Role.id_role == 2)
            {
                ViewBag.Msg = msg;
                ViewBag.List = book.getAll();
                return View();
            } else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        // Add modal form
        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            
            var file = Request.Files["file"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(file.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Content/assets/img/" + postedFileName);
            file.SaveAs(path);
            var loaisach = Int32.Parse(form["loaisach"]);
            var nxb = Int32.Parse(form["nxb"]);
            var price = float.Parse(form["price"]);
            var year = Int32.Parse(form["yearpub"]);
            var name = form["name"];
            var author = form["author"];
            var description = form["noidung"];
            var quantity = Int32.Parse(form["quantity"]);
            DateTime createdAt = DateTime.Now;
            book.add(name, author, nxb, loaisach, year, price, description, postedFileName, quantity, createdAt);
            return RedirectToAction("Index", new { msg = "1" });
        }

        // Edit detail of information
        [HttpPost]
        public ActionResult Update(FormCollection form)
        {
            var loaisach = Int32.Parse(form["loaisach"]);
            var nxb = Int32.Parse(form["nxb"]);
            var price = float.Parse(form["price"]);
            var year = Int32.Parse(form["yearpub"]);
            var name = form["name"];
            var author = form["author"];
            var description = form["noidung"];
            var bookid = Int32.Parse(form["id_book"]);
            var quantity = Int32.Parse(form["quantity"]);

            book.update(name,author,nxb,loaisach,year,price,description,quantity,bookid);
            return RedirectToAction("Index", new { msg = "1" });
        }

        // Delede information of book
        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            Book bookNew = new Book();
            bookNew.id_book = Convert.ToInt32(form["id"]);
            book.delete(bookNew.id_book);
            return RedirectToAction("Index", new { msg = "1" });
        }
        
            
    }
}