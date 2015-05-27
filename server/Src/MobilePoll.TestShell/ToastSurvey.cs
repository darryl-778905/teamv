using MobilePoll.DataModel;
using MobilePoll.MessageContracts;

namespace MobilePoll.TestShell
{
    public static class ToastSurvey
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
                    Answers = new [] {"No"},
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },
                new SurveyQuestion
                {
                    Id = 4,
                    Answers = new [] {"Because it is awesome"},
                    Limits = 250,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "Freeform",
                    Question = "What do you like to eat on toast and why?"
                },
                new SurveyQuestion
                {
                    Id = 5,
                    Answers = new [] {"No"},
                    Limits = 3,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "YesNo",
                    Question = "Do you like toast?"
                },
                new SurveyQuestion
                {
                    Id = 6,
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
