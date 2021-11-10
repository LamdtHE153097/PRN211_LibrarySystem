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
    public class PostDao
    {
        LibraryDbContext myDb = new LibraryDbContext();
        public List<Post> getAll()
        {
            return myDb.posts.OrderBy(p => p.id_post).ToList();
        }
        public Post getInformationById(int id)
        {
            return myDb.posts.Where(l => l.id_post == id).FirstOrDefault();
        }
        public Post getLatestPost()
        {
            return myDb.posts.OrderByDescending(x => x.id_post).FirstOrDefault();
        }
        public void add(string title, string content, int id_publisher, DateTime createdAt, int id_user)
        {
            string sql = @"insert into Posts(title,content,id_publisher,createdAt,id_user) values 
                    (@tittle, @content, @id_pub, @createdAt, @id_user)";
            myDb.Database.ExecuteSqlCommand(sql, new SqlParameter("@tittle",title),
                new SqlParameter("@content", content),
                new SqlParameter("@id_pub", id_publisher),
                new SqlParameter("@createdAt", createdAt),
                new SqlParameter("@id_user", id_user));
        }
        public void update(string title, string content, int id_publisher, int id_post)
        {

            string sql = @"update dbo.Posts set title = @title, content = @content, 
                        id_publisher = @id_publisher where id_post = @id_post";

            myDb.Database.ExecuteSqlCommand(sql, new SqlParameter("@title", title),
                new SqlParameter("@content", content),
                new SqlParameter("@id_publisher", id_publisher),
                new SqlParameter("@id_post", id_post)
                );
        }
        public void delete(int id_post)
        {
            var result = myDb.posts.Where(x => x.id_post == id_post).SingleOrDefault();
            myDb.posts.Remove(result);
            myDb.SaveChanges();
        }
    }
}