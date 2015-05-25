using System.Collections.Generic;
using MobilePoll.Application.Parsers;
using MobilePoll.DataModel;

namespace MobilePoll.Application
{
    public class ParserPipeline
    {
        private readonly List<QuestionParser> pipeline = new List<QuestionParser>();

        public ParserPipeline AddParser(QuestionParser parser)
        {
            pipeline.Add(parser);
            return this;
        }

        public void ParseSurvey(Survey survey)
        {
            foreach (var surveyQuestion in survey.Questions)
            {
                ParseQuestion(surveyQuestion);
            }
        }

        private void ParseQuestion(SurveyQuestion surveyQuestion)
        {
            foreach (var questionParser in pipeline)
            {
                if (questionParser.Parse(surveyQuestion))
                {
                    break;
                }
            }
        }
    }
}