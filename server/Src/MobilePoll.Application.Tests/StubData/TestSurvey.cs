using MobilePoll.DataModel;

namespace MobilePoll.Application.Tests.StubData
{
    public static class TestSurvey
    {
        public static Survey Survey()
        {
            SurveyQuestion[] questions = {
                new SurveyQuestion
                {
                    Id = 1,
                    Answers = new [] {"Yes"},
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },
                new SurveyQuestion
                {
                    Id = 3,
                    Answers = new [] {"Because it is awesome"},
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