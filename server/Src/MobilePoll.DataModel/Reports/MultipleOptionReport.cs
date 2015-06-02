using System;
using System.Collections.Generic;

namespace MobilePoll.DataModel.Reports
{
    public class MultipleOptionReport
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public string Question { get; set; }
        public Dictionary<string, int> SelectedOptions { get; set; }
    }
}