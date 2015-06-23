using System;
using System.Linq;
using MobilePoll.Application.Commands;
using MobilePoll.Bus;
using MobilePoll.DataModel;
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
                return;

            surveys.Add(e.Survey);
        }
    }
}
