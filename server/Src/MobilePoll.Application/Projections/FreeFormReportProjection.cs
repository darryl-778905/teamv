using System;
using System.Collections.Generic;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel.Reports;
using MobilePoll.MessageContracts.Events;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Projections
{
    public class FreeFormReportProjection : IHandleEvent<FreeFormAnswerReceived>
    {
        private readonly IRepository<FreeFormReport> reports;

        public FreeFormReportProjection(IRepositoryFactory repoFactory)
        {
            this.reports = repoFactory.GetRepository<FreeFormReport>();
        }

        public void When(FreeFormAnswerReceived e)
        {
            if(String.IsNullOrWhiteSpace(e.Answer))
                return;

            var report = GetReport(e);
            
            report.Answers.Add(e.Answer);

            SaveReport(report);
        }

        private void SaveReport(FreeFormReport report)
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

        private FreeFormReport GetReport(FreeFormAnswerReceived e)
        {
            var report = reports.FirstOrDefault(r => r.SurveyName == e.SurveyName && r.Question == e.Question);

            if (report == null)
            {
                report = new FreeFormReport();
                report.Answers = new List<string>();
                report.Question = e.Question;
                report.SurveyName = e.SurveyName;
            }

            return report;
        }
    }
}