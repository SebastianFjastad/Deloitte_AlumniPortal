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
    public class SpecialsRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Special> GetSpecials()
        {
            return db.Specials.Include(a => a.Image).ToList();
        }

        public Special GetSpecial(int id)
        {
            return db.Specials.Include(i => i.Image).SingleOrDefault(i => i.SpecialId == id);
        }

        public bool SaveSpecial(Special special, HttpPostedFileBase upload)
        {
            //handle image if exists
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new Asset
                {
                    AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                    FileType = FileType.Special,
                    ContentType = upload.ContentType,
                };

                string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Specials/");
                string targetPath = Path.Combine(targetFolder, image.AssetName);
                upload.SaveAs(targetPath);

                special.Image =  image;
                special.CreatedDate = DateTime.Now;
            }

            db.Specials.Add(special);
            db.SaveChanges();
            return false;
        }

        public bool EditSpecial(Special special, HttpPostedFileBase upload)
        {
            Special specialToUpdate = db.Specials.Include(i => i.Image).SingleOrDefault(i => i.SpecialId ==     special.SpecialId);

            //if the image has changed
            if (upload != null && upload.ContentLength > 0)
            {
                if (specialToUpdate != null)
                {
                        var asset = specialToUpdate.Image;
                        //delete the file from Assets folder
                        string path = HttpContext.Current.Server.MapPath("~/Assets/Specials/" + asset.AssetName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        //delete the file path object from the DB
                        db.Assets.Remove(specialToUpdate.Image);
                        db.SaveChanges();

                    var image = new Asset
                    {
                        //ArticleId = article.ArticleID,
                        AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                        FileType = FileType.Special
                    };

                    string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Specials/");
                    string targetPath = Path.Combine(targetFolder, image.AssetName);
                    upload.SaveAs(targetPath);

                    specialToUpdate.Image =  image;
                }
            }

            specialToUpdate.Title = special.Title;
            specialToUpdate.Body = special.Body;
            specialToUpdate.SpecialUrl = special.SpecialUrl;
            //specialToUpdate.ValidFrom = special.ValidFrom;
            //specialToUpdate.ValidTo = special.ValidTo;

            db.SaveChanges();
            return false;
        }

        public bool Delete(int id)
        {
            var specialToDelete = db.Specials.Find(id);
            db.Specials.Remove(specialToDelete);
            db.SaveChanges();
            return false;
        }
    }
}