using System;

namespace MobilePoll.DataModel.Reports
{
    public class PercentageOfPeopleWhoLikeToastPerYear
    {
        public Guid Id { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public int Total { get; set; }
        public int Year { get; set; }
        public decimal PercentYes { get; set; }
        public decimal PercentNo { get { return 100M - PercentYes; } }
    }
}