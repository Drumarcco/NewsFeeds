using System.Web;

namespace NewsFeeds.Web.Util
{
    public class CurrentUser : ICurrentUser
    {
        public bool IsLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}