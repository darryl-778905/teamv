using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MobilePoll.Application.Commands;
using MobilePoll.Bus;
using MobilePoll.DataModel;
using MobilePoll.Persistence;

namespace MobilePoll.Web.Api.Controllers
{
    public class PollResultController : ApiController
    {
        private readonly ILocalBus bus;
        private readonly IRepository<PollResult> pollResults;

        public PollResultController(IRepositoryFactory repositoryFactory, ILocalBus bus)
        {
            this.bus = bus;
            pollResults = repositoryFactory.GetRepository<PollResult>();
        }

        // GET api/values 
        public IEnumerable<PollResult> Get()
        {
            return pollResults.ToArray();
        } 

        // GET api/values/5 
        public PollResult Get(Guid id)
        {
            return pollResults.Get(id);
        }        

        // POST api/values 
        public Guid Post([FromBody] Survey value)
        {
            Guid id = Guid.NewGuid();
            bus.Execute(new SavePollResult(id, value));

            return id;
        }
    }
}