using System;
using System.Collections.Generic;

namespace MobilePoll.DataModel.Reports
{
    public class FreeFormReport
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
    }
}