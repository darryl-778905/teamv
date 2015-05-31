using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel.Reports;
using MobilePoll.MessageContracts.Events;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Projections
{
    public class PercentageOfPeopleWhoLikeToastProjection :
        IHandleEvent<YesNoAnswerReceived>,
        IHandleEvent<FreeformAnswerReceived>
    {
        private readonly IRepository<PercentageOfPeopleWhoLikeToast> reports;

        public PercentageOfPeopleWhoLikeToastProjection(IRepositoryFactory repoFactory)
        {
            this.reports = repoFactory.GetRepository<PercentageOfPeopleWhoLikeToast>();
        }

        public void When(YesNoAnswerReceived e)
        {
            var report = reports.FirstOrDefault() ?? new PercentageOfPeopleWhoLikeToast();

            report.Total++;

            if (e.Result)
            {
                report.Yes++;
            }
            else
            {
                report.No++;
            }

            report.PercentYes = report.Yes / (decimal)report.Total * 100M;


            if (report.Id == Guid.Empty)
            {
                report.Id = Guid.NewGuid();
                reports.Add(report);
            }
            else
            {
                reports.Update(report);
            }
        }

        public void When(FreeformAnswerReceived e)
        {
 
        }
    }
}
