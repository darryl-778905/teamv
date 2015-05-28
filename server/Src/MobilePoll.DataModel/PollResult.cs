using System;
using MobilePoll.MessageContracts;

namespace MobilePoll.DataModel
{
    public class PollResult
    {
        public Guid Id { get; set; }
        public Survey Result { get; set; }
    }
}