using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts;

namespace MobilePoll.Application.Parsers
{
    public abstract class QuestionParser
    {
        public ILocalBus Bus { get; set; }

        protected abstract string Type { get; }
        protected abstract bool IsMulipleOptionQuestion { get; }
        protected abstract void ExtractData(Guid surveyId, string surveyName, SurveyQuestion question);

        public virtual bool Parse(Guid surveyId, string surveyName, SurveyQuestion question)
        {
            Guard.ParameterNotNull(question, "question");
            
            if(!Type.Equals(question.Type, StringComparison.CurrentCultureIgnoreCase))
                return false;

            MandatoryAnswerCheck(question);
            
            if (QuestionContainsAnswers(question))
            {
                if (!IsMulipleOptionQuestion)
                {
                    Guard.ParameterCondition(question.Answers.First().Length <= question.Limits, "question.answers");
                }
                ExtractData(surveyId, surveyName, question);
                
            }

            return true;
        }

        private static void MandatoryAnswerCheck(SurveyQuestion question)
        {
            if (question.Mandatory)
            {
                if (QuestionContainsAnswers(question))
                {
                    return;
                }
                throw new ArgumentException("mandatory question must have an answer");
            }
        }

        protected static bool QuestionContainsAnswers(SurveyQuestion question)
        {
            return question.Answers != null && question.Answers.Length > 0;
        }
    }
}