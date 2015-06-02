using System;
using System.Linq;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts;
using MobilePoll.MessageContracts.Events;

namespace MobilePoll.Application.Parsers
{
    public class YesNoQuestionParser : QuestionParser
    {
        protected override string Type
        {
            get { return "YesNo"; }
        }

        protected override bool IsMulipleOptionQuestion
        {
            get { return false; }
        }

        protected override void ExtractData(Guid surveyId, string surveyName, SurveyQuestion question)
        {
            string answer = question.Answers.First();
            bool result = answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase);

            YesNoAnswerReceived answerReceived = new YesNoAnswerReceived
            {
                SurveyId = surveyId,
                SurveyName = surveyName,
                Question = question.Question,
                QuestionId = question.QuestionNumber,
                Result = result
            };

            Bus.Raise(answerReceived);
        }
    }
}