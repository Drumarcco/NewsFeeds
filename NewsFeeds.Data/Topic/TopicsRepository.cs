﻿using NewsFeeds.Data.Context;
using NewsFeeds.Entities.Post.ViewModels;
using NewsFeeds.Entities.Topic;
using NewsFeeds.Entities.Topic.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NewsFeeds.Data.Topic
{
    public class TopicsRepository : ITopicsRepository
    {
        public TopicDisplayViewModel GetTopic(string topicName, string queryPosts)
        {
            using (var context = new ApplicationDbContext())
            {
                var topic = context.Topics
                     .Include(t => t.Subscriptions)
                     .Include(t => t.Posts)
                     .Include(t => t.Posts.Select(p => p.Author))
                     .FirstOrDefault(t => t.Name.ToLower() == topicName.ToLower());

                if (topic == null)
                {
                    throw new TopicNotFoundException(topicName);
                }

                return new TopicDisplayViewModel
                {
                    Name = topic.Name,
                    Posts = MapTopicPosts(topic).Where(GetPostsQueryPredicate(queryPosts)).ToList(),
                    SubscriptionsCount = topic.Subscriptions.Count
                };
            }
        }

        public List<TopicDisplayViewModel> GetTopics()
        {
            using (var context = new ApplicationDbContext())
            {
                List<TopicModel> topics = new List<TopicModel>();
                topics = context.Topics
                    .Include(t => t.Subscriptions)
                    .Include(t => t.Posts)
                    .Include(t => t.Posts.Select(p => p.Author))
                    .ToList();

                if (topics != null)
                {
                    List<TopicDisplayViewModel> topicsDisplay = new List<TopicDisplayViewModel>();

                    foreach (var topic in topics)
                    {
                        var topicDisplay = new TopicDisplayViewModel
                        {
                            Name = topic.Name,
                            SubscriptionsCount = topic.Subscriptions.Count,
                            Posts = MapTopicPosts(topic)
                        };
                        topicsDisplay.Add(topicDisplay);
                    }

                    return topicsDisplay;
                }

                return null;
            }
        }

        public List<UserTopicViewModel> GetTopicsWithUserContext(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var topics = new List<TopicModel>();

                topics = context.Topics
                    .Include(t => t.Subscriptions)
                    .Include(t => t.Posts)
                    .Include(t => t.Posts.Select(p => p.Author))
                    .ToList();

                if (topics != null)
                {
                    List<UserTopicViewModel> userTopics = new List<UserTopicViewModel>();

                    foreach (var topic in topics)
                    {
                        var userTopic = new UserTopicViewModel
                        {
                            Name = topic.Name,
                            Posts = MapTopicPosts(topic),
                            SubscriptionsCount = topic.Subscriptions.Count,
                            IsSubscribed = topic.Subscriptions.Any(s => s.UserId == userId)
                        };

                        userTopics.Add(userTopic);
                    }

                    return userTopics;
                }

                return null;
            }
        }

        public UserTopicViewModel GetTopicWithUserContext(string userId, string topicName, string queryPosts)
        {
            using (var context = new ApplicationDbContext())
            {
                var topic = context.Topics
                    .Include(t => t.Subscriptions)
                    .Include(t => t.Posts)
                    .Include(t => t.Posts.Select(p => p.Author))
                    .FirstOrDefault(t => t.Name.ToLower() == topicName.ToLower());

                if (topic == null)
                {
                    throw new TopicNotFoundException(topicName);
                }

                return new UserTopicViewModel
                {
                    Name = topic.Name,
                    Posts = MapTopicPosts(topic).Where(GetPostsQueryPredicate(queryPosts)).ToList(),
                    SubscriptionsCount = topic.Subscriptions.Count,
                    IsSubscribed = topic.Subscriptions.Any(s => s.UserId == userId)
                };
            }
        }

        private System.Func<PostDisplayViewModel, bool> GetPostsQueryPredicate(string query = "")
        {
            if (query == null)
            {
                query = "";
            }

            var queryLowercase = query.ToLower();
            return p => p.Title.ToLower().Contains(queryLowercase)
                || p.Content.ToLower().Contains(queryLowercase)
                || p.AuthorUsername.ToLower().Contains(queryLowercase);
        }

        private List<PostDisplayViewModel> MapTopicPosts(TopicModel topic)
        {
            return topic.Posts.Select(p => new PostDisplayViewModel
            {
                Content = p.Content,
                Date = p.PostedAt.ToShortDateString(),
                Time = p.PostedAt.ToShortTimeString(),
                Title = p.Title,
                TopicName = topic.Name,
                AuthorUsername = p.Author.UserName
            }).ToList();
        }
    }
}
