using System;
using MobilePoll.Bus;
using MobilePoll.DataModel;

namespace MobilePoll.Application.Commands
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
