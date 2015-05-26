using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePoll.Application.Parsers;
using MobilePoll.Application.Tests.StubData;
using MobilePoll.Application.Tests.Stubs;
using Shouldly;

namespace MobilePoll.Application.Tests
{
    [TestClass]
    public class ParserPipelineTest
    {
        private ParserPipeline pipeline;
        private LocalBusStub bus = new LocalBusStub();

        [TestInitialize]
        public void Initialize()
        {
            pipeline = new ParserPipeline();
            
            pipeline
                .AddParser(new YesNoQuestionParser(bus))
                .AddParser(new FreeformQuestionParser(bus));
        }

        [TestMethod]
        public void Pipeline_parses_survey_correctly()
        {
            pipeline.ParseSurvey(TestSurvey.Survey());
            bus.RaisedEvents.Count.ShouldBe(2);
        }
    }
}