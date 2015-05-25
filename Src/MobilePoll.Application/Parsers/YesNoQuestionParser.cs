using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts.Events;

namespace MobilePoll.Application.Parsers
{
    public class YesNoQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "YesNo"; }
        }

        public YesNoQuestionParser(ILocalBus bus)
            : base(bus)
        {
        }

        protected override void ExtractData(SurveyQuestion question)
        {
            if (QuestionContainsAnswers(question))
            {
                string answer = question.Answers.First();
                bool result = answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase);

                YesNoAnswerReceived answerReceived = new YesNoAnswerReceived
                {
                    Question = question.Question,
                    QuestionId = question.Id,
                    Result = result
                };

                Bus.Raise(answerReceived);
            }
        }
    }
}