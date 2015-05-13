using System.Web.Http;
using MobilePoll.DataModel;
using MobilePoll.DataModel.TestData;
using MobilePoll.Infrastructure.Logging;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Ioc;
using MobilePoll.Logging;
using MobilePoll.Persistence;
using MobilePoll.Web.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using LogLevel = MobilePoll.Infrastructure.Logging.LogLevel;

namespace MobilePoll.Web.Api.Configuration
{
    public class Startup
    {
        public static readonly IConfigurationModule DefaultConfiguration = new InMemoryConfiguration();

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureLogging(config);
            ConfigureJsonResponseFormat(config);
            ConfigureDefaultRoutes(config);
            IntializeIoc(config);
            IntializeDebugData();
            appBuilder.UseWebApi(config);
        }

        private static void ConfigureLogging(HttpConfiguration config)
        {
            ConsoleWindowLogger.MinimumLogLevel = LogLevel.Info;
            LogFactory.BuildLogger = type => new ConsoleWindowLogger(type);
            config.Filters.Add(new LoggingFilterAttribute());
        }

        private static void ConfigureJsonResponseFormat(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;
            json.SerializerSettings.Formatting = Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        private static void ConfigureDefaultRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional } );
        }

        private static void IntializeIoc(HttpConfiguration config)
        {
            var autofacAdapter = new WebApiAutofacAdapter();
            autofacAdapter.RegisterConfigurationModule(DefaultConfiguration);  //this is where we will change our environment configuration settings.
            MobilePoll.Environment.Configuration.Initialize(autofacAdapter);
            config.DependencyResolver = autofacAdapter.GetApiDependencyResolver();
        }

        /// <summary>
        /// Configure debug surveys, this will be removed once we can populate 
        /// </summary>
        private void IntializeDebugData()
        {
            using (var lifetimeScope = Environment.Configuration.RootContainer.BeginLifetimeScope())
            {
                IRepositoryFactory repositoryFactory = lifetimeScope.GetInstance<IRepositoryFactory>();
                IRepository<Survey> surveys = repositoryFactory.GetRepository<Survey>();

                foreach (var defaultSurvey in DefaultSurveys.Surveys)
                {
                    surveys.Add(defaultSurvey);
                }
            }
        }

    }
}