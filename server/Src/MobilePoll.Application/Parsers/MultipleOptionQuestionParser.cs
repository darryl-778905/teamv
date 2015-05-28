using MobilePoll.MessageContracts;
using MobilePoll.MessageContracts.Events;

namespace MobilePoll.Application.Parsers
{
    public class MultipleOptionQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "MultiOption"; }
        }

        protected override bool IsMulipleOptionQuestion
        {
            get { return true; }
        }

        protected override void ExtractData(int surveyId, string surveyName, SurveyQuestion question)
        {
            var answerReceived = new MultipleOptionAnswerReceived
            {
                SurveyId = surveyId,
                SurveyName = surveyName,
                Question = question.Question,
                QuestionId = question.Id,
                Result = question.Answers,
            };

            Bus.Raise(answerReceived);
        }
    }
}