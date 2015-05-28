using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilePoll.DataModel.Reports
{
    public class PercentageOfPeopleWhoLikeToast
    {
        public int Id { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public int Total { get; set; }
        public decimal PercentYes { get; set; }
        public decimal PercentNo { get { return 100M - PercentYes; }}
    }
}
