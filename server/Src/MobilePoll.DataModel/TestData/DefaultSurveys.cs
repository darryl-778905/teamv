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
            Defaults = new[] { HospitalSurvey(), YesNoSurvey(), MultiOptionSurvey(), FreeformSurvey(), ToastSurvey() };
        }

        private static Survey YesNoSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    QuestionNumber = 1,
                    Answers = new string[0],
                    Limits = 3,
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
                    QuestionNumber = 1,
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
                    QuestionNumber = 1,
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
                    QuestionNumber = 1,
                    Answers = new string[0],
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },                
                new SurveyQuestion
                {
                    QuestionNumber = 3,
                    Answers = new string[0],
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What do you like to eat on toast and why?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 2,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Light", "Dark", "Burnt", "French"},
                    Type = "MultiOption",
                    Question = "Which of these type of toast do you like?"
                },
            };

            return new Survey
            {
                Name = "Toast SelectedOptions",
                Description = "Tell us about how you like your toast.",
                Questions = questions
            };
        }

        private static Survey HospitalSurvey()
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    QuestionNumber = 1,
                    Answers = new string[0],
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Have you visited a hospital in the last year?"
                },        
                new SurveyQuestion
                {
                    QuestionNumber = 2,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"Black", "White", "Indian", "Coloured"},
                    Type = "MultiOption",
                    Question = "Which ethnic group do you most identity with?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 3,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"1 or less", "2-4" ,"5-9", "10 or more"},
                    Type = "MultiOption",
                    Question = "How many times did you visit the hospital in the last year?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 4,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"Male", "Female", "Transgender"},
                    Type = "MultiOption",
                    Question = "What is your Gender?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 5,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"25 or Less", "26-39", "45-54", "55-64", "65 or over" },
                    Type = "MultiOption",
                    Question = "What is your Age?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 6,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"Gauteng", "Mpumalanga", "Eastern Cape", "Western Cape", "Northern Cape", "North-West", "Freestate", "KwaZulu", "Limpopo"  },
                    Type = "MultiOption",
                    Question = "In which provice do you reside?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 7,
                    Answers = new string[0],
                    Limits = 1,
                    Mandatory = true,
                    Options = new[] {"1", "2", "3", "4", "5"},
                    Type = "MultiOption",
                    Question = "Rate your overall experience, 1 being poor and 5 being excellent."
                },
                new SurveyQuestion
                {
                    QuestionNumber = 8,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Long waiting times", "Unprofessional staff", "Poor sanitary conditions", "Unavailability of staff", "Lack of signage", "Unvailability of medication"},
                    Type = "MultiOption",
                    Question = "Which of the following problems did you encounter?"
                },
                new SurveyQuestion
                {
                    QuestionNumber = 9,
                    Answers = new string[0],
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What is the one improvement you would suggest"
                },
            };

            return new Survey
            {
                Name = "Hospital Service Delivery Survey",
                Description = "A survey to determine the effectiveness of service delivery in communities.",
                Questions = questions
            };
        }
    }
}