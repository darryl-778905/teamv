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
        public string Get(int id)
        {
            BuildTestSurvey(id);
            return String.Format("Survey {0} added", id);
        }        

        // POST api/values 
        public void Post([FromBody]string value) 
        { 
        } 

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value) 
        { 
        } 

        // DELETE api/values/5 
        public void Delete(int id) 
        { 
        }

        private void BuildTestSurvey(int id)
        {
            SurveyQuestion[] questions = new[]
            {
                new SurveyQuestion
                {
                    Id = id,
                    Answers = new string[0],
                    Limits = 0,
                    Mandatory = true,
                    Options = new string[0],
                    Type = "FreeForm",
                    Question = "What do you like to eat on toast and why?"
                }
            };

            Survey survey = new Survey
            {
                Description = "Some Description",
                Name = "My Survey",
                Questions = questions
            };

            surveys.Add(survey);
        }
    }
}