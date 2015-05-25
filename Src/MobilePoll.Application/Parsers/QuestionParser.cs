using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel;

namespace MobilePoll.Application.Parsers
{
    public abstract class QuestionParser
    {
        protected readonly ILocalBus Bus;
        protected abstract string Type { get; }
        protected abstract void ExtractData(SurveyQuestion question);

        protected QuestionParser(ILocalBus bus)
        {
            this.Bus = bus;
        }

        public virtual bool Parse(SurveyQuestion question)
        {
            Guard.ParameterNotNull(question, "question");
            
            if(!Type.Equals(question.Type, StringComparison.CurrentCultureIgnoreCase))
                return false;

            MandatoryAnswerCheck(question);
            
            if (QuestionContainsAnswers(question))
            {
                Guard.ParameterCondition(question.Answers.First().Length <= question.Limits, "question.answers");
                ExtractData(question);
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