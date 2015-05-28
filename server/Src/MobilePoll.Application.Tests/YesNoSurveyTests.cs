using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePoll.Application.Parsers;
using MobilePoll.Application.Tests.StubData;
using MobilePoll.Application.Tests.Stubs;
using MobilePoll.MessageContracts.Events;
using Shouldly;

namespace MobilePoll.Application.Tests
{
    [TestClass]
    public class YesNoSurveyTests
    {
        private YesNoQuestionParser parser;
        private LocalBusStub bus;

        [TestInitialize]
        public void Initialize()
        {
            bus = new LocalBusStub();
            parser = new YesNoQuestionParser();
            parser.Bus = bus;
        }

        [TestMethod]
        public void Parser_must_be_able_to_identify_a_yesno_question()
        {
            parser.Parse(1, "TestSurvey", TestQuestions.YesnoQuestion);
            bus.EventTypeWasRaised<YesNoAnswerReceived>().ShouldBe(true);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Parser_must_fail_if_question_is_null()
        {
            parser.Parse(1, "TestSurvey", null);
        }

        [TestMethod]
        public void Question_must_contain_answer_if_mandatory()
        {
            parser.Parse(1, "TestSurvey", TestQuestions.YesnoQuestion);
            bus.EventTypeWasRaised<YesNoAnswerReceived>().ShouldBe(true);
        }

        [TestMethod]
        public void Answer_event_is_raised()
        {
            parser.Parse(1, "TestSurvey", TestQuestions.YesnoQuestion);
           
            bus.EventTypeWasRaised<YesNoAnswerReceived>().ShouldBe(true);
            var answer = bus.GetFirstEventOfType<YesNoAnswerReceived>();
            answer.Result.ShouldBe(true);
        }
    }
}