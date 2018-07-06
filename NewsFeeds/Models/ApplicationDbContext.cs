using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NewsFeeds.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}