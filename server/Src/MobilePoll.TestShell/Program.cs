using MobilePoll.Application;
using MobilePoll.Application.Parsers;
using MobilePoll.DataModel.TestData;
using MobilePoll.Environment;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Wireup;
using MobilePoll.Ioc;
using MobilePoll.Logging;

namespace MobilePoll.TestShell
{
    class Program
    {
        static void Main(string[] args)
        {
            Intialize();
            ParserPipeline pipeline = InitializePipeline();
            pipeline.ParseSurvey(TestPollResult.Result());
        }

        private static void Intialize()
        {
            ConsoleWindowLogger.MinimumLogLevel = LogLevel.Debug;
            LogFactory.BuildLogger = type => new ConsoleWindowLogger(type);
            IContainerBuilder containerBuilder = new AutofacAdapter();
            containerBuilder.RegisterConfigurationModule(new InMemoryConfiguration());
            Configuration.Initialize(containerBuilder);
            ServiceLocator.Current.SetCurrentLifetimeScope(Configuration.RootContainer);
        }

        public static ParserPipeline InitializePipeline()
        {
            ParserPipeline.AddParser(new FreeformQuestionParser());
            ParserPipeline.AddParser(new YesNoQuestionParser());
            ParserPipeline.AddParser(new MultipleOptionQuestionParser());

            return new ParserPipeline(Configuration.Bus);
        }
    }
}
