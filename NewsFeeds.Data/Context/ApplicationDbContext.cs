using Microsoft.AspNet.Identity.EntityFramework;
using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;
using System.Data.Entity;

namespace NewsFeeds.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public DbSet<TopicModel> Topics { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
