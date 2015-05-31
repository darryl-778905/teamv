using System;

namespace MobilePoll.MessageContracts.Events
{
    public class FreeformAnswerReceived : ISurveyAnswerEvent
    {
        public Guid SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Result { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}