using System;
using System.Collections.Generic;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel.Reports;
using MobilePoll.MessageContracts.Events;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Projections
{
    public class MultipleOptionReportProjection : IHandleEvent<MultipleOptionAnswerReceived>
    {
        private readonly IRepository<MultipleOptionReport> reports;

        public MultipleOptionReportProjection(IRepositoryFactory repoFactory)
        {
            this.reports = repoFactory.GetRepository<MultipleOptionReport>();
        }

        public void When(MultipleOptionAnswerReceived e)
        {
            if(e.SelectedOptions == null || e.SelectedOptions.Length == 0)
                return;

            var report = GetReport(e);

            foreach (string selectedOption in e.SelectedOptions)
            {
                if (!report.SelectedOptions.ContainsKey(selectedOption))
                {
                    report.SelectedOptions[selectedOption] = 1;
                }
                else
                {
                    report.SelectedOptions[selectedOption]++;
                }
            }

            SaveReport(report);
        }

        private void SaveReport(MultipleOptionReport report)
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

        private MultipleOptionReport GetReport(MultipleOptionAnswerReceived e)
        {
            var report = reports.FirstOrDefault(r => r.SurveyName == e.SurveyName && r.Question == e.Question);

            if (report == null)
            {
                report = new MultipleOptionReport();
                report.Question = e.Question;
                report.SelectedOptions = new Dictionary<string, int>();
                report.SurveyName = e.SurveyName;
            }

            return report;
        }
    }
}