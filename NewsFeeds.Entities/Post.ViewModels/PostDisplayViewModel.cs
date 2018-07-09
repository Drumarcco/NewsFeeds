namespace NewsFeeds.Entities.Post.ViewModels
{
    public class PostDisplayViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string AuthorUsername { get; set; }
        public string TopicName { get; set; }
    }
}
