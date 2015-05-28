﻿using MobilePoll.DataModel;
using MobilePoll.MessageContracts;

namespace MobilePoll.Application.Tests.StubData
{
    public static class TestQuestions
    {
        public static readonly SurveyQuestion FreeformQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new string[0],
            Limits = 250,
            Mandatory = false,
            Options = new string[0],
            Type = "Freeform",
            Question = "What is your name?"
        };

        public static readonly SurveyQuestion MandatoryFreeformQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new[] { "Derrick" },
            Limits = 250,
            Mandatory = true,
            Options = new string[0],
            Type = "Freeform",
            Question = "What is your name?"
        };

        public static readonly SurveyQuestion FreeformLimitQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new [] {"Derrick"},
            Limits = 1,
            Mandatory = true,
            Options = new string[0],
            Type = "Freeform",
            Question = "What is your name?"
        };


        public static readonly SurveyQuestion MultipleOptionQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new[] { "Red", "Green" },
            Limits = 0,
            Mandatory = true,
            Options = new[] {"Red", "Green", "Blue", "Black"},
            Type = "MultiOption",
            Question = "Which of these colours do you like?"
        };

        public static readonly SurveyQuestion NoAnswerMultipleOptionQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new string[0],
            Limits = 0,
            Mandatory = true,
            Options = new[] { "Red", "Green", "Blue", "Black" },
            Type = "MultiOption",
            Question = "Which of these colours do you like?"
        };

        public static readonly SurveyQuestion YesnoQuestion = new SurveyQuestion
        {
            Id = 1,
            Answers = new []{"Yes"},
            Limits = 3,
            Mandatory = true,
            Options = new string[0],
            Type = "YesNo",
            Question = "Are you alive?"
        };

    }
}