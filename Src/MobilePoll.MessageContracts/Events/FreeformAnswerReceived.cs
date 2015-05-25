using MobilePoll.Bus;

namespace MobilePoll.MessageContracts.Events
{
    public class FreeformAnswerReceived : IEvent
    {
        public string Result { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}