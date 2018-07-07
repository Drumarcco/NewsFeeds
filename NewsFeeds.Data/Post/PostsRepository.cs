using NewsFeeds.Data.Context;
using NewsFeeds.Entities.Post.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace NewsFeeds.Data.Post
{
    public class PostsRepository : IPostsRepository
    {
        public List<PostDisplayViewModel> GetUserNewsFeed(string UserId)
        {
            using (var context = new ApplicationDbContext())
            {
                var posts = context.Subscriptions
                    .Include(s => s.Topic)
                    .Include(s => s.Topic.Posts)
                    .Where(s => s.UserId == UserId)
                    .Select(s => s.Topic.Posts)
                    .SelectMany(p => p)
                    .Include(p => p.Author)
                    .OrderByDescending(p => p.PostedAt)
                    .ToList();

                var displayPosts = new List<PostDisplayViewModel>();

                foreach (var post in posts)
                {
                    displayPosts.Add(new PostDisplayViewModel
                    {
                        Title = post.Title,
                        Content = post.Content,
                        Date = post.PostedAt.ToShortDateString(),
                        Time = post.PostedAt.ToShortTimeString(),
                        TopicName = post.TopicName,
                        AuthorUsername = post.Author.UserName
                    });
                }



                return displayPosts;
            }
        }
    }
}
