using System;

namespace MobilePoll.MessageContracts
{
    public class Survey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SurveyQuestion[] Questions { get; set; }
    }
}
