using System;

namespace MobilePoll.MessageContracts.Events
{
    public class MultipleOptionAnswerReceived : ISurveyAnswerEvent
    {
        public Guid SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string[] SelectedOptions { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}