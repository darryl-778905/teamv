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
            var report = reports.FirstOrDefault();

            if (report == null)
            {
                report = new PercentageOfPeopleWhoLikeToast();
                reports.Add(report);
            }

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
        }

        public void When(FreeformAnswerReceived e)
        {
 
        }
    }
}
