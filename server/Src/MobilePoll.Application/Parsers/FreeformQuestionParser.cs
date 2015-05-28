using System.Linq;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts;
using MobilePoll.MessageContracts.Events;

namespace MobilePoll.Application.Parsers
{
    public class FreeformQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "Freeform"; }
        }

        protected override bool IsMulipleOptionQuestion
        {
            get { return false; }
        }

        protected override void ExtractData(int surveyId, string surveyName, SurveyQuestion question)
        {
            string answer = question.Answers.First();

            var answerReceived = new FreeformAnswerReceived
            {
                SurveyId = surveyId,
                SurveyName = surveyName,
                Question = question.Question,
                QuestionId = question.Id,
                Result = answer,
            };

            Bus.Raise(answerReceived);
        }
    }
}