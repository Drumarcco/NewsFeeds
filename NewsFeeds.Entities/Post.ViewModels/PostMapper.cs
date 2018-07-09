namespace NewsFeeds.Entities.Post.ViewModels
{
    public class PostMapper
    {
        static public PostDisplayViewModel Map(PostModel post)
        {
            return new PostDisplayViewModel
            {
                Id = post.Id,
                AuthorUsername = post.Author.UserName,
                Content = post.Content,
                Date = post.PostedAt.ToShortDateString(),
                Time = post.PostedAt.ToShortDateString(),
                Title = post.Title,
                TopicName = post.TopicName
            };
        }
    }
}
