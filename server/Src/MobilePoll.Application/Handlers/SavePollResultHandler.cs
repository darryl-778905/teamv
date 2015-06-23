using MobilePoll.Application.Commands;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Handlers
{
    public class SavePollResultHandler : IHandleCommand<SavePollResult>
    {
        private readonly ParserPipeline parserPipeline;
        private readonly IRepository<PollResult> pollResults;

        public SavePollResultHandler(IRepositoryFactory repositoryFactory, ParserPipeline parserPipeline)
        {
            this.parserPipeline = parserPipeline;
            pollResults = repositoryFactory.GetRepository<PollResult>();
        }

        public void Execute(SavePollResult e)
        {
            parserPipeline.ParseSurvey(e.Survey);

            var pollResult = new PollResult
            {
                Id = e.Id,
                Result = e.Survey
            };

            pollResults.Add(pollResult);
        }
    }
}