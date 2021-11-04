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
        public List<Book> getFiveBook()
        {
            return myDb.books.OrderByDescending(p => p.id_book).Take(5).ToList();
        }
        public void add(string name, string author, int id_publisher, int id_category, int year_publish, float price, string description, string image, DateTime createdAt)
        {
            string sql = "insert into Books(name,author,id_publisher,id_category,year_publish,price,description,image,createdAt) values(N'" + name + "',N'" + author + "','" + id_publisher + "','" + id_category + "','" + year_publish + "','" + price + "',N'" + description + "',N'" + image + "','" + createdAt + "')";
            myDb.Database.ExecuteSqlCommand(sql);
        }

        public void delete(int id_book)
        {
            var result = myDb.books.Where(x => x.id_book == id_book).SingleOrDefault();
            myDb.books.Remove(result);
            myDb.SaveChanges();
        }

        public void update(string name, string author, int id_publisher, int id_category, int year_publish, float price, string description, int id_book)
        {
            
            string sql = @"update dbo.Books set [name] = @name, author = @author , id_category = @id_cat,
                    id_publisher = @id_pub, year_publish = @year, price = @price, [description] = @descrip 
                    where id_book = @id_book
                    ";

            myDb.Database.ExecuteSqlCommand(sql, new SqlParameter("@name", name),
                new SqlParameter("@author", author),
                new SqlParameter("@id_cat", id_category),
                new SqlParameter("@id_pub", id_publisher),
                new SqlParameter("@year", year_publish),
                new SqlParameter("@price", price),
                new SqlParameter("@descrip", description),
                new SqlParameter("@id_book", id_book)
                );
        }
    }
}