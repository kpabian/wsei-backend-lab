using ApplicationCore.Interfaces.AdminService;
using AutoMapper;
using BackendLab01;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiQuizAdminController : ControllerBase
    {
        private readonly IQuizAdminService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<QuizItem> _validator;

        public ApiQuizAdminController(IQuizAdminService service, IMapper mapper, IValidator<QuizItem> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public ActionResult<object> AddQuiz(LinkGenerator link, NewQuizDto dto)
        {
            var quiz = _service.AddQuiz(_mapper.Map<Quiz>(dto));
            return Created(
                link.GetPathByAction(HttpContext, nameof(GetQuiz), null, new { quiId = quiz.Id }),
                quiz
            );
        }

        [HttpGet]
        [Route("{quizId}")]
        public ActionResult<Quiz> GetQuiz(int quizId)
        {
            var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
            return quiz is null ? NotFound() : quiz;
        }

        [HttpPatch]
        [Route("{quizId}")]
        [Consumes("application/json-patch+json")]
        public ActionResult<Quiz> AddQuizItem(int quizId, [FromBody] JsonPatchDocument<Quiz>? patchDoc)
        {
            var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
            if (quiz is null || patchDoc is null)
            {
                return NotFound(new
                {
                    error = $"Quiz width id {quizId} not found"
                });
            }
            int previousCount = quiz.Items.Count;
            patchDoc.ApplyTo(quiz);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (previousCount < quiz.Items.Count)
            {
                QuizItem item = quiz.Items[^1];
                var validationResult = _validator.Validate(item);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                quiz.Items.RemoveAt(quiz.Items.Count - 1);
                _service.AddQuizItemToQuiz(quizId, item);
            }
            return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
        }
    }
}
