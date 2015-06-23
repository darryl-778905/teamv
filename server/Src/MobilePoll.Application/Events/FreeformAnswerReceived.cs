using System;

namespace MobilePoll.Application.Events
{
    public class FreeFormAnswerReceived : ISurveyAnswerEvent
    {
        public Guid SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}