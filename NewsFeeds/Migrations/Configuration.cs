namespace NewsFeeds.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using NewsFeeds.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsFeeds.Models.ApplicationDbContext>
    {

        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Topics.AddOrUpdate(new Topic { Name = "dogs" });
            context.Topics.AddOrUpdate(new Topic { Name = "cats" });
            context.Topics.AddOrUpdate(new Topic { Name = "music" });
            context.Topics.AddOrUpdate(new Topic { Name = "food" });

            userManager.Create(new ApplicationUser { UserName = "defaultuser", Email = "defaultuser@mail.com" }, "defaultuser");
            userManager.Create(new ApplicationUser { UserName = "johndoe", Email = "johndoe@mail.com" }, "johndoe");
            userManager.Create(new ApplicationUser { UserName = "janedoe", Email = "janedoe@mail.com" }, "janedoe");

            var user = userManager.FindByName("defaultuser");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "dogs", UserId = user.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "food", UserId = user.Id });

            var johndoe = userManager.FindByName("johndoe");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "cats", UserId = johndoe.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "music", UserId = johndoe.Id });

            var janedoe = userManager.FindByName("janedoe");
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "dogs", UserId = janedoe.Id });
            context.Subscriptions.AddOrUpdate(new Subscription { TopicName = "music", UserId = janedoe.Id });

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