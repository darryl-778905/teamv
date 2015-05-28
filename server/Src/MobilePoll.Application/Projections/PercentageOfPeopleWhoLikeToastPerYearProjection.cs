﻿using System;
using System.Linq;
using MobilePoll.Bus;
using MobilePoll.DataModel.Reports;
using MobilePoll.MessageContracts.Events;
using MobilePoll.Persistence;

namespace MobilePoll.Application.Projections
{
    public class PercentageOfPeopleWhoLikeToastPerYearProjection : 
        IHandleEvent<YesNoAnswerReceived>, 
        IHandleEvent<FreeformAnswerReceived>
    {
        private readonly IRepository<PercentageOfPeopleWhoLikeToastPerYear> reports;

        public PercentageOfPeopleWhoLikeToastPerYearProjection(IRepositoryFactory repoFactory)
        {
            reports = repoFactory.GetRepository<PercentageOfPeopleWhoLikeToastPerYear>();
        }

        public void When(YesNoAnswerReceived e)
        {
            var report = reports.FirstOrDefault(r => r.Year == DateTime.Now.Year);

            if (report == null)
            {
                report = new PercentageOfPeopleWhoLikeToastPerYear
                {
                    Year = DateTime.Today.Year
                };

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

            report.PercentYes = report.Yes / (decimal)report.Total *100M;
        }

        public void When(FreeformAnswerReceived e)
        {
        }
    }
}