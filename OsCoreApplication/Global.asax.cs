using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using OsCoreApplication.App_Start;
using OsCoreApplication.Configuration;

namespace OsCoreApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region Configuration Autofac

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();
            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new MapperModule());
            builder.RegisterModule(new LibModule());

            builder.Register(s => new ActionFilterConfig(s.Resolve<Services.ITransactionProvider>()))
                .AsActionFilterFor<IController>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
