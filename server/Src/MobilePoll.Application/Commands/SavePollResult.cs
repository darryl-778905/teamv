using System;
using MobilePoll.Bus;
using MobilePoll.DataModel;

namespace MobilePoll.Application.Commands
{
    public class SavePollResult : ICommand
    {
        public Guid Id { get; private set; }
        public Survey Survey { get; private set; }

        public SavePollResult(Guid id, Survey survey)
        {
            Id = id;
            Survey = survey;
        }
    }
}