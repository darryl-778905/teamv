using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MobilePoll.Bus;
using MobilePoll.MessageContracts;
using MobilePoll.MessageContracts.Commands;
using MobilePoll.Persistence;

namespace MobilePoll.Web.Api.Controllers
{
    public class SurveyController : ApiController
    {
        private readonly ILocalBus bus;
        private readonly IRepository<Survey> surveys; 

        public SurveyController(IRepositoryFactory repositoryFactory, ILocalBus bus)
        {
            this.bus = bus;
            surveys = repositoryFactory.GetRepository<Survey>();
        }

        // GET api/values 
        public IEnumerable<Survey> Get()
        {
            return surveys.ToArray();
        }

        // GET api/values 
        public Survey Get(Guid id)
        {
            return surveys.Get(id);
        } 

        // POST api/values 
        public Guid Post([FromBody]Survey value)
        {
            Guid newid = Guid.NewGuid();

            bus.Execute(new RegisterNewSurvey(value, newid));

            return newid;
        } 
    }
}