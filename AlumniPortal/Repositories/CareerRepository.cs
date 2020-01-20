using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Repositories
{
    public class CareerRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Career> GetCareers()
        {
            return db.Careers.ToList();
        }

        public Career GetCareer(int id)
        {
            return db.Careers.Find(id);
        }

        public bool SaveCareer(Career career)
        {
            career.CreatedDate = DateTime.Now;
            db.Careers.Add(career);
            db.SaveChanges();
            return false;
        }

        public bool EditCareer(Career career)
        {
            db.Entry(career).State = EntityState.Modified;
            db.SaveChanges();
            return false;
        }

        public bool Delete(int id)
        {
            var careerToDelete = db.Careers.Find(id);
            db.Careers.Remove(careerToDelete);
            db.SaveChanges();
            return false;
        }
    }
}