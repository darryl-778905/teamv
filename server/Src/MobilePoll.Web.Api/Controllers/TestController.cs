using System.Web.Http;
using MobilePoll.Bus;

namespace MobilePoll.Web.Api.Controllers
{
    public class TestController : ApiController
    {
        private readonly ILocalBus bus;

        public TestController(ILocalBus bus)
        {
            //this is simply done to ensure the dependency injection is working.
            this.bus = bus;
        }

        // GET api/values 
        public string Get()
        {
            return "Connection established";
        }
    }
} 

