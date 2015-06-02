using System;
using MobilePoll.Bus;

namespace MobilePoll.MessageContracts
{
    public interface ISurveyAnswerEvent : IEvent
    {
        Guid SurveyId { get; set; }
        string SurveyName { get; set; }
        string Question { get; set; }
        int QuestionId { get; set; }
    }
}