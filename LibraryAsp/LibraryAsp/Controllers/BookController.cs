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
            ViewBag.Msg = msg;
            ViewBag.List = book.getAll();
            return View();
        }
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
            DateTime createdAt = DateTime.Now;
            book.add(name, author, nxb, loaisach, year, price, description, postedFileName, createdAt);
            return RedirectToAction("Index", new { msg = "1" });
        }
    }
}