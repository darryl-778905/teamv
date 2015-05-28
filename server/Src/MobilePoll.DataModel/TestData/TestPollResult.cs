using MobilePoll.MessageContracts;

namespace MobilePoll.DataModel.TestData
{
    public static class TestPollResult
    {
        public static Survey Result()
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
                    Id = 2,
                    Answers = new [] {"Because it is awesome"},
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What do you like to eat on toast and why?"
                },
                new SurveyQuestion
                {
                    Id = 3,
                    Answers = new[] {"Light", "Dark"},
                    Limits = 0,
                    Mandatory = true,
                    Options = new[] {"Light", "Dark", "Burnt", "French"},
                    Type = "MultiOption",
                    Question = "How do you like your toast?"
                }
            };

            return new Survey
            {
                Name = "Toast Result",
                Description = "Tell us about how you like your toast.",
                Questions = questions
            };
        }
    }
}
