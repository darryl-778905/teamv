using System.Collections.Generic;
using MobilePoll.MessageContracts;

namespace MobilePoll.DataModel.TestData
{
    public static class DefaultSurveys
    {
        private static readonly Survey[] Defaults;

        public static IEnumerable<Survey> Surveys { get { return Defaults; } }

        static DefaultSurveys()
        {
            Defaults = new[] { YesNoSurvey(), MultiOptionSurvey(), FreeformSurvey(), ToastSurvey() };
        }

        private static Survey YesNoSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Are you alive?"
                },
            };

            return new Survey
            {
                Name = "Yes No",
                Description = "Yes/No test survey",
                Questions = questions
            };
        }

        private static Survey MultiOptionSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Red", "Green", "Blue", "Black"},
                    Type = "MultiOption",
                    Question = "Which of these colours do you like?"
                },
            };

            return new Survey
            {
                Name = "Multi Option",
                Description = "Multi option test survey",
                Questions = questions
            };
        }

        private static Survey FreeformSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new string[0],
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What is your name?"
                },
            };

            return new Survey
            {
                Name = "Freeform",
                Description = "Freeform test survey",
                Questions = questions
            };
        }

        private static Survey ToastSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },
                new SurveyQuestion
                {
                    Id = 2,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Light", "Dark", "Burnt", "French"},
                    Type = "MultiOption",
                    Question = "Which of these type of toast do you like?"
                },
                new SurveyQuestion
                {
                    Id = 3,
                    Answers = new string[0],
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What do you like to eat on toast and why?"
                },
            };

            return new Survey
            {
                Name = "Toast Survey",
                Description = "Tell us about how you like your toast.",
                Questions = questions
            };
        }
    }
}