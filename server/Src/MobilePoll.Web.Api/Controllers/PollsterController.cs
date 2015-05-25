using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MobilePoll.DataModel;
using MobilePoll.Persistence;

namespace MobilePoll.Web.Api.Controllers
{
    public class PollsterController : ApiController
    {
        private readonly IRepository<Polster> polsters;

        public PollsterController(IRepositoryFactory repositoryFactory)
        {
            polsters = repositoryFactory.GetRepository<Polster>();
        }

        public IEnumerable<Polster> Get()
        {
            return polsters.ToArray();
        }

        public int Post([FromBody] Polster polster)
        {
            Guard.ParameterNotNull(polster, "pollster");

            polsters.Add(polster);

            return polster.Id;
        }
    }
}