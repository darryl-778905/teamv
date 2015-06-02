using System.Dynamic;
using System.Linq;
using System.Web.Http;
using MobilePoll.DataModel.Reports;
using MobilePoll.Persistence;

namespace MobilePoll.Web.Api.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IRepository<YesNoReport> yesNoReports;
        private readonly IRepository<MultipleOptionReport> multipleOptionReports;
        private readonly IRepository<FreeFormReport> freeFormReports;

        public ReportController(IRepositoryFactory repositoryFactory)
        {
            yesNoReports = repositoryFactory.GetRepository<YesNoReport>();
            multipleOptionReports = repositoryFactory.GetRepository<MultipleOptionReport>();
            freeFormReports = repositoryFactory.GetRepository<FreeFormReport>();
        }

        // GET api/values 
        public ReportDto Get()
        {
            ReportDto report = new ReportDto
            {
                YesNoReport = yesNoReports.ToArray(),
                MultipleOptionReport = multipleOptionReports.ToArray(),
                FreeFormReport = freeFormReports.ToArray()
            };

            return report;
        }
    }

    public class ReportDto
    {
        public YesNoReport[] YesNoReport { get; set; }
        public MultipleOptionReport[] MultipleOptionReport { get; set; }
        public FreeFormReport[] FreeFormReport { get; set; }
    }
}