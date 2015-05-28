using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using MobilePoll.Infrastructure.Ioc;

namespace MobilePoll.Web.Api.Wireup
{
    public class WebApiAutofacAdapter : AutofacAdapter
    {
        public WebApiAutofacAdapter()
            : base(ConfigureApiDependencies())
        {
        }

        private static IContainer ConfigureApiDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinderProvider();
            return builder.Build();
        }

        public AutofacWebApiDependencyResolver GetApiDependencyResolver()
        {
            return new AutofacWebApiDependencyResolver(LifetimeScope);
        }
    }
}