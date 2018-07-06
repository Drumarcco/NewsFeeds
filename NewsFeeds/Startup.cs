using Microsoft.Owin;
using NewsFeeds.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsFeeds.Startup))]
namespace NewsFeeds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            applicationDbContext.Database.Initialize(true);
        }
    }
}
