using System;
using System.Web.Http;
using MobilePoll.Application.Wireup;
using MobilePoll.DataModel.TestData;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Logging;
using MobilePoll.MessageContracts.Commands;
using MobilePoll.Web.Api.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace MobilePoll.Web.Api.Wireup
{
    public class Startup
    {
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
            autofacAdapter.RegisterConfigurationModule(new InMemoryConfiguration());  
            autofacAdapter.RegisterConfigurationModule(new ParserPipelineConfiguration());

            Environment.Configuration.Initialize(autofacAdapter);
            config.DependencyResolver = autofacAdapter.GetApiDependencyResolver();
        }

        /// <summary>
        /// Configure debug surveys, this will be removed once we can populate 
        /// </summary>
        private void IntializeDefaultData()
        {
            foreach (var defaultSurvey in TestSurveys.Surveys)
            {
                Environment.Configuration.Bus.Execute(new RegisterNewSurvey(defaultSurvey));
            }

            Environment.Configuration.Bus.Execute(new SavePollResult(Guid.NewGuid(), TestPollResult.Result()));
        }
    }
}