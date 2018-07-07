using Microsoft.Owin;
using NewsFeeds.Data.Context;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsFeeds.Web.Startup))]
namespace NewsFeeds.Web
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
