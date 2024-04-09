using BackendLab01;
using gRPC.DTO;
using Microsoft.AspNetCore.Mvc;

namespace gRPC.Controllers;


[ApiController]
[Route(["/api/v1/user/quizzes"])]
public class APIUserQuizController : ControllerBase
{
    private IQuizUserService _userService;
    public APIUserQuizController(IQuizUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route($"{id}")]
    public ActionResult<QuizDTO?> GetQuizById(int id)
    {
        var result = _userService.FindQuizById(id);
        if(result is null)
        {
            return NotFound();
        }
        return QuizDTO.Of(result);
    }

    [HttpPost]
    [Route($"{quizId}/items/{itemId}/answers")]
    public ActionResult SaveAnswer(int quizId, int itemId, QuizItemUserAnswerDTO answer)
    {
        _userService.SaveUserAnswerForQuiz(quizId, itemId, answer.Answer);
        return Created();
    }
}
