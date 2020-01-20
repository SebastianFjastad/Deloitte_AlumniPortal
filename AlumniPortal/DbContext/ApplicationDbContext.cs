using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AlumniPortal.Entities;
using AlumniPortal.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlumniPortal.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EntityDbContext", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventInvite> EventInvites { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Alumnus> Alumni { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Special> Specials { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Article>().HasMany(a => a.Assets).WithOptional(x => x.Article).HasForeignKey(z => z.ArticleId).WillCascadeOnDelete();
            modelBuilder.Entity<Album>().HasMany(a => a.Images).WithOptional(x => x.Album).HasForeignKey(z => z.AlbumId).WillCascadeOnDelete();
            modelBuilder.Entity<Alumnus>().HasOptional(a => a.Asset).WithOptionalDependent(x => x.Alumnus);
            modelBuilder.Entity<ApplicationUser>().HasOptional(a => a.ProfilePic).WithOptionalDependent(x => x.User);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
