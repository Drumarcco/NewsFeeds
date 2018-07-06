using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsFeeds.Startup))]
namespace NewsFeeds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
