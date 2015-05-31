using System;

namespace MobilePoll.DataModel.Reports
{
    public class YesNoReport
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public string Question { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public int Total { get; set; }
        public decimal PercentYes { get; set; }
    }
}
