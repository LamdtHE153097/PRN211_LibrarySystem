using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LibraryAsp.Models;

namespace LibraryAsp.Dao
{
    
    public class CategoryDao
    {
        LibraryDbContext myDb = new LibraryDbContext();
        public List<Category> getAll()
        {
            return myDb.categories.OrderByDescending(p => p.id_category).ToList();
        }
        public void add(Category category)
        {
            myDb.categories.Add(category);
            myDb.SaveChanges();
        }
       
        }
public void edit(Category category)
        {
            string sql = "update dbo.Categories set [name] = @nameCat where id_category = @idCat";

            myDb.Database.ExecuteSqlCommand(sql, new SqlParameter("@nameCat", category.name),
                new SqlParameter("@idCat", category.id_category)
            );
        }

        public void delete(int catId)
        {
            var result = myDb.categories.Where(x => x.id_category == catId).SingleOrDefault();
            myDb.categories.Remove(result);
            myDb.SaveChanges();
        }
    }
}