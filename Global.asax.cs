using System;
using System.Linq;
using System.Web.Http;
using Financeasy.Api.Core.DI;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Financeasy.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Container _container;

        protected void Application_Start()
        {
            _container = new Container();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsDefined(typeof(Register), false))
                .Select(Activator.CreateInstance).ToList();

            _container.Options.PropertySelectionBehavior = new PropertyInject<Inject>();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.RegisterCollection(types);
            _container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public static T GetInstance<T>() where T : class => _container.GetInstance<T>();

        public static void Stop() => _container.Dispose();
    }
}
