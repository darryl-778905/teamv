using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePoll.Application.Parsers;
using MobilePoll.Application.Tests.StubData;
using MobilePoll.Application.Tests.Stubs;

namespace MobilePoll.Application.Tests
{
    [TestClass]
    public class FreeformSurveyTests
    {
        private FreeformQuestionParser parser;
        private LocalBusStub bus;
        private static readonly Guid Id = Guid.NewGuid();

        [TestInitialize]
        public void Initialize()
        {
            bus = new LocalBusStub();
            parser = new FreeformQuestionParser();
            parser.Bus = bus;
        }

        [TestMethod]
        public void Parser_must_be_able_to_identify_a_freeform_question()
        {
            parser.Parse(Id, "TestSurvey", TestQuestions.FreeformQuestion);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void Parser_must_fail_if_text_longer_than_limit()
        {
            parser.Parse(Id, "TestSurvey", TestQuestions.FreeformLimitQuestion);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Parser_must_fail_if_question_is_null()
        {
            parser.Parse(Id, "TestSurvey", null);
        }

        [TestMethod]
        public void Question_must_contain_answer_if_mandatory()
        {
            parser.Parse(Id, "TestSurvey", TestQuestions.MandatoryFreeformQuestion);
        }
    }
}
