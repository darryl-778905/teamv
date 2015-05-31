using System;
using MobilePoll.Bus;

namespace MobilePoll.MessageContracts.Commands
{
    public class RegisterNewSurvey : ICommand
    {
        public Survey Survey { get; private set; }

        public RegisterNewSurvey(Survey survey, Guid id)
        {
            Survey = survey;
            Survey.Id = id;
        }
    }
}
