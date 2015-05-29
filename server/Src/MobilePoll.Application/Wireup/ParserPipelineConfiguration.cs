using MobilePoll.Application.Parsers;
using MobilePoll.Ioc;

namespace MobilePoll.Application.Wireup
{
    public class ParserPipelineConfiguration : IConfigurationModule
    {
        public void Configure(IContainerBuilder containerBuilder)
        {
            ParserPipeline.AddParser(new FreeformQuestionParser());
            ParserPipeline.AddParser(new YesNoQuestionParser());
            ParserPipeline.AddParser(new MultipleOptionQuestionParser());

            containerBuilder.RegisterType<ParserPipeline>();
        }
    }
}
