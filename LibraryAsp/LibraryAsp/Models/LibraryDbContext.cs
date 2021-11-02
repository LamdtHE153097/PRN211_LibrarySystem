using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryAsp.Models
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("QuanLyDBConnectionString")
        {

        }
        public DbSet<User> users { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<Transaction> transactions { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Book> books { get; set; }

        public DbSet<Publisher> publishers { get; set; }
        public DbSet<Post> posts { get; set; }
    }
}