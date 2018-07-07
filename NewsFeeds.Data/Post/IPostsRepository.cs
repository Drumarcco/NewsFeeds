using NewsFeeds.Entities.Post.ViewModels;
using System.Collections.Generic;

namespace NewsFeeds.Data.Post
{
    public interface IPostsRepository
    {
        List<PostDisplayViewModel> GetUserNewsFeed(string UserId);
    }
}
