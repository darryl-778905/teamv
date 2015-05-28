using System;
using System.Collections.Generic;
using MobilePoll.Application.Parsers;
using MobilePoll.Bus;
using MobilePoll.MessageContracts;

namespace MobilePoll.Application
{
    public class ParserPipeline
    {
        private static readonly List<QuestionParser> Pipeline = new List<QuestionParser>();

        public ILocalBus Bus { get; set; }

        public ParserPipeline()
        {
        }

        public ParserPipeline(ILocalBus bus)
        {
            Bus = bus;
        }
        
        public static void AddParser(QuestionParser parser)
        {
            Pipeline.Add(parser);
        }

        public void ParseSurvey(Survey survey)
        {
            foreach (var surveyQuestion in survey.Questions)
            {
                ParseQuestion(survey.Id, survey.Name, surveyQuestion);
            }
        }

        private void ParseQuestion(int surveyId, string surveyName, SurveyQuestion surveyQuestion)
        {
            foreach (var questionParser in Pipeline)
            {
                questionParser.Bus = Bus;

                if (questionParser.Parse(surveyId, surveyName, surveyQuestion))
                {
                    return;
                }
            }

            string errorMessage = String.Format("No parser module found for survey question type : {0}", surveyQuestion.Type);
            throw new InvalidSurveyQuestionTypeException(errorMessage);
        }
    }
}