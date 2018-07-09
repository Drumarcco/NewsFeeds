using NewsFeeds.Data.Context;
using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Entities.Post;
using NewsFeeds.Entities.Subscription;
using NewsFeeds.Entities.Topic;
using System;

namespace NewsFeeds.Data.Generic
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private IRepository<TopicModel> topicRepository;
        private IRepository<SubscriptionModel> subscriptionRepository;
        private IRepository<PostModel> postRepository;
        private IRepository<ApplicationUserModel> userRepository;

        public IRepository<TopicModel> TopicRepository
        {
            get
            {

                if (topicRepository == null)
                {
                    topicRepository = new Repository<TopicModel>(context);
                }
                return topicRepository;
            }
        }

        public IRepository<SubscriptionModel> SubscriptionRepository
        {
            get
            {

                if (subscriptionRepository == null)
                {
                    subscriptionRepository = new Repository<SubscriptionModel>(context);
                }
                return subscriptionRepository;
            }
        }

        public IRepository<PostModel> PostRepository
        {
            get
            {
                if (postRepository == null)
                {
                    postRepository = new Repository<PostModel>(context);
                }
                return postRepository;
            }
        }

        public IRepository<ApplicationUserModel> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new Repository<ApplicationUserModel>(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
