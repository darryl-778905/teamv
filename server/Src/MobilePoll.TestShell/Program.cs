using MobilePoll.Application;
using MobilePoll.Application.Parsers;
using MobilePoll.Environment;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Persistence;
using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Ioc;

namespace MobilePoll.TestShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Intialize();
            ParserPipeline pipeline = InitializePipeline();

            pipeline.ParseSurvey(ToastSurvey.Survey());

            var serializer = new JsonObjectSerializer();

            string text = serializer.Serialize(InMemoryUnitOfWork.WorkingSet);
        }

        private static void Intialize()
        {
            IContainerBuilder containerBuilder = new AutofacAdapter();
            containerBuilder.RegisterConfigurationModule(new InMemoryConfiguration());
            Configuration.Initialize(containerBuilder);
            ServiceLocator.Current.SetCurrentLifetimeScope(Configuration.RootContainer);
        }

        public static ParserPipeline InitializePipeline()
        {
            ParserPipeline pipeline = new ParserPipeline(Configuration.Bus);
            
            pipeline
                .AddParser(new FreeformQuestionParser())
                .AddParser(new YesNoQuestionParser());

            return pipeline;
        }
    }
}
