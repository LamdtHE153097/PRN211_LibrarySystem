using LibraryAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAsp.Dao
{
    public class PublisherDao
    {
        LibraryDbContext myDb = new LibraryDbContext();
        public List<Publisher> getAll()
        {
            return myDb.publishers.OrderByDescending(p => p.id_publisher).ToList();
        }
        public Publisher getInformationById(int id)
        {
            return myDb.publishers.Where(l => l.id_publisher == id).FirstOrDefault();
        }
        public void add(Publisher publisher)
        {
            myDb.publishers.Add(publisher);
            myDb.SaveChanges();
        }
        public void edit(Publisher publisher)
        {
            var obj = getInformationById(publisher.id_publisher);
            obj.name = publisher.name;
            myDb.SaveChanges();
        }
        public void delete(int id)
        {
            var obj = getInformationById(id);
            myDb.publishers.Remove(obj);
            myDb.SaveChanges();
        }
    }
}