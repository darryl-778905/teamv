using System;
using System.Collections.Generic;
using MobilePoll.Application.Parsers;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.MessageContracts;

namespace MobilePoll.Application
{
    public class ParserPipeline
    {
        private readonly List<QuestionParser> pipeline = new List<QuestionParser>();

        public ILocalBus Bus { get; set; }

        public ParserPipeline()
        {
        }

        public ParserPipeline(ILocalBus bus)
        {
            Bus = bus;
        }
        
        public ParserPipeline AddParser(QuestionParser parser)
        {
            pipeline.Add(parser);
            return this;
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
            foreach (var questionParser in pipeline)
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