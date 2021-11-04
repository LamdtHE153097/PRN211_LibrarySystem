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
    }
}