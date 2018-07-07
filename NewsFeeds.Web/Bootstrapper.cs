using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using NewsFeeds.Data.Context;
using NewsFeeds.Data.Topic;
using NewsFeeds.Entities.ApplicationUser;
using NewsFeeds.Web.Controllers;
using System.Data.Entity;
using System.Web.Mvc;
using Unity.Mvc4;

namespace NewsFeeds.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ITopicsRepository, TopicsRepository>();
            container.RegisterType<IUserStore<ApplicationUserModel>, UserStore<ApplicationUserModel>>();
            container.RegisterType<UserManager<ApplicationUserModel>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}