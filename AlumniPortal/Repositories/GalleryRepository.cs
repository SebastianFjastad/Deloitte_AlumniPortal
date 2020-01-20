using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Repositories
{
    public class GalleryRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Album> GetAlbums()
        {
            return db.Albums.Include(a => a.CoverImage).ToList();
        }

        public Album GetAlbum(int id)
        {
            return db.Albums.Include(a => a.CoverImage).Include(i => i.Images).FirstOrDefault(a => a.AlbumId == id);
        }

        public bool CreateAlbum(Album album, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new Asset
                {
                    AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                    FileType = FileType.AlbumCover,
                    ContentType = upload.ContentType,
                };

                string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Albums/");
                string targetPath = Path.Combine(targetFolder, image.AssetName);
                upload.SaveAs(targetPath);

                album.CoverImage = image;
            }

            album.CreatedDate = DateTime.Now;

            db.Albums.Add(album);
            db.SaveChanges();
            return false;
        }

        public Album EditAlbum(Album model, HttpPostedFileBase upload)
        {
            var albumToUpdate = db.Albums.Include(a => a.CoverImage).Include(i => i.Images).FirstOrDefault(a => a.AlbumId == model.AlbumId);

            if (upload != null)
            {
                string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Albums/");

                if (albumToUpdate.CoverImage != null)
                {
                    //delete cover image
                    string path = HttpContext.Current.Server.MapPath("~/Assets/Albums/" + albumToUpdate.CoverImage.AssetName);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    db.Assets.Remove(albumToUpdate.CoverImage);
                    db.SaveChanges();
                }

                //new album cover
                var image = new Asset
                {
                    AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                    FileType = FileType.AlbumCover
                };

                string targetPath = Path.Combine(targetFolder, image.AssetName);
                upload.SaveAs(targetPath);

                albumToUpdate.CoverImage = image;
            }

            albumToUpdate.Name = model.Name;
            db.SaveChanges();    

            return albumToUpdate;
        }

        public bool SaveImages(int? albumId, IEnumerable<HttpPostedFileBase> uploads)
        {
            var albumToUpdate = db.Albums.Include(a => a.Images).FirstOrDefault(x => x.AlbumId == albumId);

            if (albumToUpdate.Images == null)
            {
                albumToUpdate.Images = new List<Asset>();
            }

            string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Albums/");

            foreach (var img in uploads)
            {
                var image = new Asset
                {
                    AssetName = Path.GetFileName(img.FileName),
                    FileType = FileType.AlbumImage
                };

                if (!File.Exists(targetFolder + image.AssetName))
                {
                    albumToUpdate.Images.Add(image);
                    string targetPath = Path.Combine(targetFolder, image.AssetName);
                    img.SaveAs(targetPath);
                    albumToUpdate.Images.Add(image);    
                }
            }
            db.SaveChanges();
            
            return false;
        }

        public List<Asset> GetImages(int albumId)
        {
            var images = db.Assets.Where(a => a.AlbumId == albumId).ToList();
            return images;
        }

        public bool DeleteAlbum(int id)
        {
            db.Albums.Include(a => a.CoverImage).Include(i => i.Images).FirstOrDefault(a => a.AlbumId == id);
            return true;
        }

        public bool DeleteImage(int id)
        {
            var assetToDelete = db.Assets.Find(id);

            string path = HttpContext.Current.Server.MapPath("~/Assets/Albums/" + assetToDelete.AssetName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            db.Assets.Remove(assetToDelete);
            db.SaveChanges();
            return true;
        }
    }
}