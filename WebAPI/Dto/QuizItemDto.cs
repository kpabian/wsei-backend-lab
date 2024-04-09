using BackendLab01;

namespace WebAPI.Dto
{
    public class QuizItemDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string[] Options { get; set; }

        public static QuizItemDto Of(QuizItem item)
        {
            List<string> options = item.IncorrectAnswers.ToList();
            options.Add(item.CorrectAnswer);
            return new QuizItemDto
            {
                Id = item.Id,
                Question = item.Question,
                Options = options.OrderBy(x => Guid.NewGuid()).ToArray()
            };
        }
    }
}
