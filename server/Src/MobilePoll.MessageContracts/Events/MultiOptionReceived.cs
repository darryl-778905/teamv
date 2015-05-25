using MobilePoll.Bus;

namespace MobilePoll.MessageContracts.Events
{
    public class MultiOptionReceived : IEvent
    {
        public string[] Result { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}