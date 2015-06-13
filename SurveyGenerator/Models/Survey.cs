using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveyGenerator.Models
{
    public class Survey
    {
        public string SurveyName { get; set; }
    }
    public enum Gender
    {
        Male =0,
        FeMale = 1
    }
}