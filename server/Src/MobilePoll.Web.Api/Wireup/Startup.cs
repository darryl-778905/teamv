using System;
using System.Web.Http;
using MobilePoll.Application.Commands;
using MobilePoll.Application.Wireup;
using MobilePoll.DataModel.TestData;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Ioc;
using MobilePoll.Logging;
using MobilePoll.Web.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MobilePoll.Web.Api.Wireup
{
    public class Startup
    {
        private static IConfigurationModule defaultConfiguration = new InMemory();

        public static IConfigurationModule DefaultConfiguration
        {
            get { return defaultConfiguration; }
            set { defaultConfiguration = value; }
        }

        static Startup()
        {
            var configuartion = Environment.Configuration.GetSetting("Configuration");

            if (String.IsNullOrWhiteSpace(configuartion))
                return; 

            if (configuartion.Equals("Mongo"))
            {
                defaultConfiguration = new Mongo();
            }
        }

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureLogging(config);
            ConfigureJsonResponseFormat(config);
            ConfigureDefaultRoutes(config);
            CorsConfig.Configure(appBuilder);
            IntializeIoc(config);
            IntializeDefaultData();
            appBuilder.UseWebApi(config);
        }

        private static void ConfigureLogging(HttpConfiguration config)
        {
            ConsoleWindowLogger.MinimumLogLevel = LogLevel.Debug;
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
            autofacAdapter.RegisterConfigurationModule(defaultConfiguration);  
            autofacAdapter.RegisterConfigurationModule(new ParserPipelineConfiguration());

            Environment.Configuration.Initialize(autofacAdapter);
            config.DependencyResolver = autofacAdapter.GetApiDependencyResolver();
        }

        /// <summary>
        /// Configure debug surveys, this will be removed once we can populate 
        /// </summary>
        private void IntializeDefaultData()
        {
            foreach (var defaultSurvey in DefaultSurveys.Surveys)
            {
                Environment.Configuration.Bus.Execute(new RegisterNewSurvey(defaultSurvey, Guid.NewGuid()));
            }
        }
    }
}