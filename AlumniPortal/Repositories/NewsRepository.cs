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
    public class NewsRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region News Articles
        public List<Article> GetArticles()
        {
            return db.Articles.Include(a => a.Assets).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Article> GetNewsArticles()
        {
                return db.Articles.Include(a => a.Assets).Where(a => a.ArticleType == ArticleType.News).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Article> GetCareerArticles()
        {
                return db.Articles.Include(a => a.Assets).Where(a => a.ArticleType == ArticleType.Career).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public Article GetArticle(int id)
        {
            return db.Articles.Include(i => i.Assets).SingleOrDefault(i => i.ArticleID == id);
        }

        public bool SaveArticle(Article article, HttpPostedFileBase upload)
        {
            //handle image if exists
            if (upload != null && upload.ContentLength > 0)
            {
                var image = new Asset
                {
                    AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                    FileType = FileType.ArticleImage,
                    ContentType = upload.ContentType,
                };

                string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Articles/");
                string targetPath = Path.Combine(targetFolder, image.AssetName);
                upload.SaveAs(targetPath);

                article.Assets = new List<Asset> { image };
            }

            article.CreatedDate = DateTime.Now;

            db.Articles.Add(article);
            db.SaveChanges();
            return false;
        }

        public bool EditArticle(Article article, HttpPostedFileBase upload)
        {
            Article articleToUpdate = db.Articles.Include(i => i.Assets).SingleOrDefault(i => i.ArticleID == article.ArticleID);

            //if the image has changed
            if (upload != null && upload.ContentLength > 0)
            {
                if (articleToUpdate != null)
                {
                    if (articleToUpdate.Assets.Any(a => a.FileType == FileType.ArticleImage))
                    {
                        var assetPath = articleToUpdate.Assets.First();
                        //delete the file from Assets folder
                        string path = HttpContext.Current.Server.MapPath("~/Assets/Articles/" + assetPath.AssetName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        //delete the file path object from the DB
                        db.Assets.Remove(articleToUpdate.Assets.First(a => a.FileType == FileType.ArticleImage));
                        db.SaveChanges();
                    }

                    var image = new Asset
                    {
                        //ArticleId = article.ArticleID,
                        AssetName = Guid.NewGuid() + Path.GetFileName(upload.FileName),
                        FileType = FileType.ArticleImage
                    };

                    string targetFolder = HttpContext.Current.Server.MapPath("~/Assets/Articles/");
                    string targetPath = Path.Combine(targetFolder, image.AssetName);
                    upload.SaveAs(targetPath);

                    articleToUpdate.Assets = new List<Asset> { image };
                }
            }

            articleToUpdate.ArticleType = article.ArticleType;
            articleToUpdate.Title = article.Title;
            articleToUpdate.SubTitle = article.SubTitle;
            articleToUpdate.Body = article.Body;

            db.SaveChanges();
            return false;
        }

        public bool Delete(int id)
        {
            var articleToDelete = db.Articles.Find(id);
            db.Articles.Remove(articleToDelete);
            db.SaveChanges();
            return false;
        }
        #endregion

        #region Youtube Vid
        public void SaveVideo(Video vid)
        {
            var vidToUpdate = db.Videos.Find(vid.VideoId);
            if (vidToUpdate == null)
            {
                db.Videos.Add(vid);
            }
            else
            {
                vidToUpdate.YouTubeId = vid.YouTubeId;
            }
            db.SaveChanges();
        }

        public Video GetVideo()
        {
            return db.Videos.FirstOrDefault();
        }
    }
    #endregion
}