using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MobilePoll.DataModel;
using MobilePoll.Persistence;

namespace MobilePoll.Web.Api.Controllers
{
    public class SurveyController : ApiController
    {
        private readonly IRepository<Survey> surveys; 

        public SurveyController(IRepositoryFactory repositoryFactory)
        {
            surveys = repositoryFactory.GetRepository<Survey>();
        }

        // GET api/values 
        public IEnumerable<Survey> Get()
        {
            return surveys.ToArray();
        } 

        // GET api/values/5 
        public Survey Get(int id)
        {
            return surveys.Get(id);
        }        

        // POST api/values 
        public void Post([FromBody]Survey value) 
        { 
        } 

        // PUT api/values/5 
        public void Put(int id, [FromBody]Survey value) 
        { 
        } 

        // DELETE api/values/5 
        public void Delete(int id) 
        { 
        }
    }
}