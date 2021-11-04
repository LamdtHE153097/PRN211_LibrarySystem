using LibraryAsp.Dao;
using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;


namespace LibraryAsp.Dao
{
    public class BookDao
    {
        LibraryDbContext myDb = new LibraryDbContext();
        public List<Book> getAll()
        {
            return myDb.books.OrderByDescending(p => p.id_book).ToList();
        }
        public void add(string name, string author, int id_publisher, int id_category, int year_publish, float price, string description, string image, DateTime createdAt)
        {
            string sql = "insert into Books(name,author,id_publisher,id_category,year_publish,price,description,image,createdAt) values(N'" + name + "',N'" + author + "','" + id_publisher + "','" + id_category + "','" + year_publish + "','" + price + "',N'" + description + "',N'" + image + "','" + createdAt + "')";
            myDb.Database.ExecuteSqlCommand(sql);
        }

       

       
}