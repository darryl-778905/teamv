using MobilePoll.Bus;

namespace MobilePoll.MessageContracts.Events
{
    public class YesNoAnswerReceived :  IEvent 
    {
        public bool Result { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}