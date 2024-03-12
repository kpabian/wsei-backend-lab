using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class QuizModel : PageModel
{
    private readonly IQuizUserService _userService;

    public QuizModel(IQuizUserService userService)
    {
        _userService = userService;
        Answers = new List<string>();
        UserAnswer = "";
    }

    [BindProperty]
    public string? Question { get; set; }
    [BindProperty]
    public List<string> Answers { get; set; }

    [BindProperty]
    public string UserAnswer { get; set; }

    [BindProperty]
    public int QuizId { get; set; }

    [BindProperty]
    public int ItemId { get; set; }

    public IActionResult OnGet(int quizId, int itemId)
    {
        QuizId = quizId;
        ItemId = itemId;

        var quiz = _userService.FindQuizById(quizId);
        var quizItem = quiz?.Items.FirstOrDefault(x => x.Id == itemId);

        Question = quizItem?.Question;
        Answers = new List<string>();

        if (quizItem is not null)
        {
            Answers.AddRange(quizItem.IncorrectAnswers);
            Answers.Add(quizItem.CorrectAnswer);
            return Page();
        }
        return RedirectToPage("Summary", new { quizId = QuizId, itemId = ItemId });
    }

    public IActionResult OnPost()
    {
        _userService.SaveUserAnswerForQuiz(QuizId, 1, ItemId, UserAnswer);
        return RedirectToPage("Item", new { quizId = QuizId, itemId = ItemId + 1 });
    }
}
