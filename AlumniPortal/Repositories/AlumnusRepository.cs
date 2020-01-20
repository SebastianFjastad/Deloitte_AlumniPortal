using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using AlumniPortal.Utilities;

namespace AlumniPortal.Repositories
{
    public class AlumnusRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Alumnus> GetAlumni()
        {
            return db.Alumni.Include(a => a.Asset).Include(a => a.User).ToList();
        }

        public Alumnus GetAlumnus(int id)
        {
            var result = db.Alumni.Include(a => a.Asset).Include(a => a.User).SingleOrDefault(a => a.AlumnusID == id);

            return result;
        }

        public List<Alumnus> GetRandomAlumni(int num)
        {
            var alumni = db.Alumni.Select(u => u.AlumnusID);
            var randAlumniIds = alumni.PickRandom(num);

            var randAlumni = randAlumniIds.Select(id => db.Alumni.Include(a => a.Asset).Include(x => x.User).FirstOrDefault(a => a.AlumnusID == id))
                .ToList();

            return randAlumni.ToList();
        }

        public List<ApplicationUser> SearchAlumni(string term)
        {
            var emailResults = db.Users.Where(u => u.Email.Contains(term)).ToList();

            var nameResults = db.Users.Where(u => u.FirstName.Contains(term) || u.LastName.Contains(term)).ToList();

            var result = new List<ApplicationUser>();

            var duplicates = from u in emailResults
                             from a in nameResults
                             where (u.Id == a.Id)
                             select a;

            var nameResultsExcludingEmail = nameResults.Except(duplicates);

            result.AddRange(emailResults);
            result.AddRange(nameResultsExcludingEmail);

            return result;
        }

        public bool SaveAlumnus(Alumnus al, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var image = new Asset
                    {
                        AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                        FileType = FileType.ProfilePic,
                        ContentType = upload.ContentType,
                    };

                    string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Alumni/");
                    string targetPath = Path.Combine(targetFolder, image.AssetName);
                    upload.SaveAs(targetPath);

                    al.Asset = image;
                }

                db.Alumni.Add(al);
                db.SaveChanges();
                return false;
            }
            catch
            {
                //log here if necessary
            }
            return false;
        }

        public bool EditAlumnus(Alumnus al, HttpPostedFileBase upload)
        {
            var alumnusToUpdate = db.Alumni.Include(i => i.Asset).Include(i => i.User).SingleOrDefault(i => i.AlumnusID == al.AlumnusID);
            var userToLink = db.Users.Find(al.UserId);


            //if the image has changed
            if (upload != null && upload.ContentLength > 0)
            {
                if (alumnusToUpdate != null)
                {
                    if (alumnusToUpdate.Asset != null)
                    {
                        var assetPath = alumnusToUpdate.Asset;
                        //delete the file from Assets folder
                        string path = HttpContext.Current.Server.MapPath("~/Assets/Alumni/" + assetPath.AssetName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        //delete the file path object from the DB
                        db.Assets.Remove(alumnusToUpdate.Asset);
                        db.SaveChanges();
                    }

                    var image = new Asset
                    {
                        AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                        FileType = FileType.ProfilePic
                    };

                    string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Alumni/");
                    string targetPath = Path.Combine(targetFolder, image.AssetName);
                    upload.SaveAs(targetPath);

                    alumnusToUpdate.Asset = image;
                }
            }

            alumnusToUpdate.FirstName = al.FirstName;
            alumnusToUpdate.LastName = al.LastName;
            alumnusToUpdate.Email = al.Email;
            alumnusToUpdate.PhoneNumber = al.PhoneNumber;
            alumnusToUpdate.JobTitle = al.JobTitle;
            alumnusToUpdate.Interview = al.Interview;
            alumnusToUpdate.Website = al.Website;
            alumnusToUpdate.ProfileLink = al.ProfileLink;
            alumnusToUpdate.User = userToLink;

            db.SaveChanges();
            return false;
        }

        public bool EditProfile(ApplicationUser user, string profilePic)
        {
            var userToUpdate = db.Users.Include(i => i.ProfilePic).FirstOrDefault(i => i.Id == user.Id);

            //if the image has changed
            if (!string.IsNullOrEmpty(profilePic))
            {
                if (userToUpdate != null)
                {
                    if (userToUpdate.ProfilePic != null)
                    {
                        var assetPath = userToUpdate.ProfilePic;
                        //delete the file from Assets folder
                        string path = HttpContext.Current.Server.MapPath("~/Assets/Alumni/" + assetPath.AssetName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        //delete the file path object from the DB
                        db.Assets.Remove(userToUpdate.ProfilePic);
                        db.SaveChanges();
                    }

                    var imageAsset = new Asset
                    {
                        AssetName = Guid.NewGuid().ToString("N") + ".jpg",
                        FileType = FileType.ProfilePic
                    };

                    string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Alumni/");
                    string targetPath = Path.Combine(targetFolder, imageAsset.AssetName);

                    byte[] bytes = Convert.FromBase64String(profilePic);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        var image = Image.FromStream(ms);
                        image.Save(targetPath);
                    }

                    userToUpdate.ProfilePic = imageAsset;
                }
            }

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.JobTitle = user.JobTitle;
            userToUpdate.About = user.About;
            userToUpdate.AllowPubEmail = user.AllowPubEmail;
            userToUpdate.AllowPubPhoneNo = user.AllowPubPhoneNo;
            userToUpdate.Region = user.Region;

            db.SaveChanges();
            return false;
        }

        public bool DeleteAlumnus(int id)
        {
            var alumnusToDelete = db.Alumni.Find(id);
            db.Alumni.Remove(alumnusToDelete);
            db.SaveChanges();
            return false;
        }
    }
}