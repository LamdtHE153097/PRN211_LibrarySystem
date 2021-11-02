using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAsp.Dao
{
    public class BookController
    {
        LibraryDbContext myDb = new LibraryDbContext();
        public List<Book> getAll()
        {
            return myDb.books.OrderByDescending(p => p.id_book).ToList();
        }
        public void add(Book book)
        {
            myDb.books.Add(book);
            myDb.SaveChanges();
        }
    }
}