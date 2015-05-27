using MobilePoll.DataModel;
using MobilePoll.MessageContracts;

namespace MobilePoll.Application.Tests.StubData
{
    public static class TestSurvey
    {
        public static Survey Survey()
        {
            SurveyQuestion[] questions =
            {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new[] {"Yes"},
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },
                new SurveyQuestion
                {
                    Id = 3,
                    Answers = new[] {"Because it is awesome"},
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What do you like to eat on toast and why?"
                },
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new[] {"Red", "Green"},
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Red", "Green", "Blue", "Black"},
                    Type = "MultiOption",
                    Question = "Which of these colours do you like?"
                }
            };

            return new Survey
            {
                Id = 1,
                Name = "Toast Survey",
                Description = "Tell us about how you like your toast.",
                Questions = questions
            };
        } 
    }
}