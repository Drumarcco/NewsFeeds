namespace NewsFeeds.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using NewsFeeds.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            context.Topics.AddOrUpdate(new Topic { Name = "dogs" });
            context.Topics.AddOrUpdate(new Topic { Name = "cats" });
            context.Topics.AddOrUpdate(new Topic { Name = "music" });
            context.Topics.AddOrUpdate(new Topic { Name = "food" });

            userManager.Create(new ApplicationUser { UserName = "defaultuser@mail.com", Email = "defaultuser@mail.com" }, "defaultuser");
            userManager.Create(new ApplicationUser { UserName = "johndoe@mail.com", Email = "johndoe@mail.com" }, "johndoe");
            userManager.Create(new ApplicationUser { UserName = "janedoe@mail.com", Email = "janedoe@mail.com" }, "janedoe");

            var user = userManager.FindByName("defaultuser@mail.com");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "dogs", UserId = user.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "food", UserId = user.Id });

            var johndoe = userManager.FindByName("johndoe@mail.com");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "cats", UserId = johndoe.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "music", UserId = johndoe.Id });

            var janedoe = userManager.FindByName("janedoe@mail.com");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "dogs", UserId = janedoe.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "music", UserId = janedoe.Id });

            context.Posts.AddOrUpdate(new Models.Post
            {
                AuthorId = johndoe.Id,
                TopicName = "cats",
                PostedAt = new DateTime(2015, 06, 21),
                Title = "Cats are awesome!",
                Content = "TIL cats are very cool, they are silent but mysterious, and they do all sorts of funny things. If it fits it sits, lol."
            });

            context.Posts.AddOrUpdate(new Models.Post
            {
                AuthorId = johndoe.Id,
                TopicName = "music",
                PostedAt = new DateTime(2016, 04, 12),
                Title = "Music is cooler than cats!",
                Content = "Ermm, a year ago I said cats are cool. Turns out music is way cooler!"
            });

            context.Posts.AddOrUpdate(new Models.Post
            {
                AuthorId = janedoe.Id,
                TopicName = "dogs",
                PostedAt = new DateTime(2017, 12, 12),
                Title = "Dogs, they're cooler than cats",
                Content = "Dogs are super loyal, they're always playful and are very good for home security. AMAZING!"
            });

            context.Posts.AddOrUpdate(new Models.Post
            {
                AuthorId = user.Id,
                TopicName = "food",
                PostedAt = DateTime.Now,
                Title = "Mexican food",
                Content = "I've come to the conclusion that Mexican good mexican food consists primarily of corn tortillas, cheese and amazing salsas"
            });

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