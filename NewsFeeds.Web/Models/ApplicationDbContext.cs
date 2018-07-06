using Microsoft.AspNet.Identity.EntityFramework;
using NewsFeeds.Web.Migrations;
using System.Data.Entity;

namespace NewsFeeds.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}