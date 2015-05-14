using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobilePoll.DataModel
{
    public class Polster
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class PercentageOfPeopleWhoVisitedHospitalInLastYear
    {
        public int Id { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public decimal Percentage { get; set; }
    }

    public class PercentageOfPeopleWhoVisitedHospitalInLastYearPerProvince
    {
        public int Id { get; set; }
        public int Yes { get; set; }
        public int No { get; set; }
        public decimal Percentage { get; set; }
        public string ProvinceName { get; set; }
    }
}
