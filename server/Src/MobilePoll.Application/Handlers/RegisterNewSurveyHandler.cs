using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.MessageContracts;
using MobilePoll.MessageContracts.Commands;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Handlers
{
    public class RegisterNewSurveyHandler : IHandleCommand<RegisterNewSurvey>
    {
        private readonly IRepository<Survey> surveys;

        public RegisterNewSurveyHandler(IRepositoryFactory repositoryFactory)
        {
            surveys = repositoryFactory.GetRepository<Survey>();
        }

        public void Execute(RegisterNewSurvey e)
        {
            var survey = surveys.FirstOrDefault(s => s.Name == e.Survey.Name);

            if(survey != null)
                throw new InvalidOperationException("The survey has already been registered");

            surveys.Add(e.Survey);
        }
    }
}
