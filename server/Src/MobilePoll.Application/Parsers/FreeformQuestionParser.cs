using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts.Events;

namespace MobilePoll.Application.Parsers
{
    public class FreeformQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "Freeform"; }
        }

        public FreeformQuestionParser(ILocalBus bus)
            : base(bus)
        {
        }

        protected override void ExtractData(SurveyQuestion question)
        {
            if (QuestionContainsAnswers(question))
            {
                string answer = question.Answers.First();

                var answerReceived = new FreeformAnswerReceived
                {
                    Question = question.Question,
                    QuestionId = question.Id,
                    Result = answer,
                };

                Bus.Raise(answerReceived);
            }
        }        
    }
}