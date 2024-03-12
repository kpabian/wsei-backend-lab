using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Index : PageModel
{
    private readonly IQuizUserService _userService;

    [BindProperty]
    public List<BackendLab01.Quiz> Quizes { get; set; }

    public Index(IQuizUserService userService)
    {
        _userService = userService;
    }

    public void OnGet()
    {
        Quizes = _userService.GetAllQuizes();
    }
}