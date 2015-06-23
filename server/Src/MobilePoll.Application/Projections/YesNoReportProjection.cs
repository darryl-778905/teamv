using System;
using System.Linq;
using MobilePoll.Application.Events;
using MobilePoll.Bus;
using MobilePoll.DataModel.Reports;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Projections
{
    public class YesNoReportProjection : IHandleEvent<YesNoAnswerReceived>
    {
        private readonly IRepository<YesNoReport> reports;

        public YesNoReportProjection(IRepositoryFactory repoFactory)
        {
            this.reports = repoFactory.GetRepository<YesNoReport>();
        }

        public void When(YesNoAnswerReceived e)
        {
            var report = GetReport(e);

            report.Total++;

            if (e.Result) report.Yes++;
            else report.No++;

            report.PercentYes = report.Yes / (decimal)report.Total * 100M;

            SaveReport(report);
        }

        private void SaveReport(YesNoReport report)
        {
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

        private YesNoReport GetReport(YesNoAnswerReceived e)
        {
            var report = reports.FirstOrDefault(r => r.SurveyName == e.SurveyName && r.Question == e.Question);

            if (report == null)
            {
                report = new YesNoReport();
                report.Question = e.Question;
                report.SurveyName = e.SurveyName;
            }

            return report;
        }
    }
}
