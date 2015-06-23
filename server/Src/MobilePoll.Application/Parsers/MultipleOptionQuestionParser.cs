using System;
using MobilePoll.Application.Events;
using MobilePoll.DataModel;

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

        protected override void ExtractData(Guid surveyId, string surveyName, SurveyQuestion question)
        {
            var answerReceived = new MultipleOptionAnswerReceived
            {
                SurveyId = surveyId,
                SurveyName = surveyName,
                Question = question.Question,
                QuestionId = question.QuestionNumber,
                SelectedOptions = question.Answers,
            };

            Bus.Raise(answerReceived);
        }
    }
}