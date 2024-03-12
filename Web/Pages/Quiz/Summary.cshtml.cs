using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    private readonly IQuizUserService _userService;

    [BindProperty]
    public int Answers { get; set; }

    [BindProperty]
    public int Correct { get; set; }

    public Summary(IQuizUserService userService)
    {
        _userService = userService;
    }

    public void OnGet(int quizId, int itemId)
    {
        var answers = _userService.GetUserAnswersForQuiz(quizId, 1);
        Correct = answers.Count(e => e.IsCorrect());
        Answers = answers.Count;
    }
}