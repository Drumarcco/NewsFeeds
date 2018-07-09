namespace NewsFeeds.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using NewsFeeds.Entities.ApplicationUser;
    using NewsFeeds.Entities.Post;
    using NewsFeeds.Entities.Subscription;
    using NewsFeeds.Entities.Topic;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUserModel>(new UserStore<ApplicationUserModel>(context));
            context.Topics.AddOrUpdate(new TopicModel { Name = "dogs" });
            context.Topics.AddOrUpdate(new TopicModel { Name = "cats" });
            context.Topics.AddOrUpdate(new TopicModel { Name = "music" });
            context.Topics.AddOrUpdate(new TopicModel { Name = "food" });
            context.Topics.AddOrUpdate(new TopicModel { Name = "tourism" });
            context.Topics.AddOrUpdate(new TopicModel { Name = "tech" });

            userManager.Create(new ApplicationUserModel { UserName = "defaultuser@mail.com", Email = "defaultuser@mail.com" }, "defaultuser");
            userManager.Create(new ApplicationUserModel { UserName = "johndoe@mail.com", Email = "johndoe@mail.com" }, "johndoe");
            userManager.Create(new ApplicationUserModel { UserName = "janedoe@mail.com", Email = "janedoe@mail.com" }, "janedoe");

            var user = userManager.FindByName("defaultuser@mail.com");
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "dogs", UserId = user.Id });
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "food", UserId = user.Id });

            var johndoe = userManager.FindByName("johndoe@mail.com");
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "cats", UserId = johndoe.Id });
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "music", UserId = johndoe.Id });

            var janedoe = userManager.FindByName("janedoe@mail.com");
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "dogs", UserId = janedoe.Id });
            context.Subscriptions.AddOrUpdate(new SubscriptionModel { TopicName = "music", UserId = janedoe.Id });

            if (context.Posts.ToList().Count == 0)
            {
                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = johndoe.Id,
                    TopicName = "cats",
                    PostedAt = new DateTime(2015, 06, 21),
                    Title = "Cats are awesome!",
                    Content = "TIL cats are very cool, they are silent but mysterious, and they do all sorts of funny things. If it fits it sits, lol."
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = johndoe.Id,
                    TopicName = "music",
                    PostedAt = new DateTime(2016, 04, 12),
                    Title = "Music is cooler than cats!",
                    Content = "Ermm, a year ago I said cats are cool. Turns out music is way cooler!"
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = janedoe.Id,
                    TopicName = "dogs",
                    PostedAt = new DateTime(2017, 12, 12),
                    Title = "Dogs, they're cooler than cats",
                    Content = "Dogs are super loyal, they're always playful and are very good for home security. AMAZING!"
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = user.Id,
                    TopicName = "food",
                    PostedAt = DateTime.Now,
                    Title = "Mexican food",
                    Content = "I've come to the conclusion that Mexican good mexican food consists primarily of corn tortillas, cheese and amazing salsas"
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = user.Id,
                    TopicName = "tourism",
                    PostedAt = DateTime.Now,
                    Title = "Beaches are fun",
                    Content = "There's nothing like taking a sun bath by the sea side"
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = janedoe.Id,
                    TopicName = "dogs",
                    PostedAt = new DateTime(2017, 12, 12),
                    Title = "Dogs can be annoying sometimes too",
                    Content = "Chihuahuas can be really really noisy!"
                });

                context.Posts.AddOrUpdate(new PostModel
                {
                    AuthorId = johndoe.Id,
                    TopicName = "music",
                    PostedAt = new DateTime(2016, 04, 12),
                    Title = "Orchestral music is relaxing",
                    Content = "Violins, Flutes, Cello, some soft drumming in the background, it's incredible!"
                });
            }


            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                var validationResults = ((List<DbEntityValidationResult>)exception.EntityValidationErrors).ToArray();

                foreach (var validation in validationResults)
                {
                    foreach (var validationError in validation.ValidationErrors)
                    {
                        throw new Exception(validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}

