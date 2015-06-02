namespace MobilePoll.MessageContracts
{
    public class SurveyQuestion
    {
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public bool Mandatory { get; set; }
        public int Limits { get; set; }
        public string[] Options { get; set; }
        public string[] Answers { get; set; }
    }
}