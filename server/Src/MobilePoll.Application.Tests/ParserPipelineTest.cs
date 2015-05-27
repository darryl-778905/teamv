using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePoll.Application.Parsers;
using MobilePoll.Application.Tests.StubData;
using MobilePoll.Application.Tests.Stubs;
using MobilePoll.MessageContracts.Events;
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
            bus = new LocalBusStub();
            pipeline = new ParserPipeline(bus);

            pipeline
                .AddParser(new YesNoQuestionParser())
                .AddParser(new FreeformQuestionParser())
                .AddParser(new MultipleOptionQuestionParser());
        }

        [TestMethod]
        public void Pipeline_parses_survey_correctly()
        {
            pipeline.ParseSurvey(TestSurvey.Survey());
            bus.RaisedEvents.Count.ShouldBe(3);
            var answer = bus.GetFirstEventOfType<YesNoAnswerReceived>();
            answer.SurveyId.ShouldBe(TestSurvey.Survey().Id);
            answer.SurveyName.ShouldBe(TestSurvey.Survey().Name);
        }
    }
}