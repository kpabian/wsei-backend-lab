namespace WebAPI.Dto
{
    public class FeedbackDto
    {
        public int QuizId { get; set; }
        public int UserId { get;  set; }
        public int TotalQuestions { get; set; }
        public List<object> Answers { get; set; }
    }
}
