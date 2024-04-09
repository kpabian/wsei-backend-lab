﻿using BackendLab01;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiQuizUserController : ControllerBase
    {
        private readonly IQuizUserService _service;

        public ApiQuizUserController(IQuizUserService service)
        {
            _service = service;
        }
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Quiz> GetQuiz(int id)
        {
            var quiz = _service.FindQuizById(id);
            return quiz == null ? NotFound() : Ok(quiz);
        }

        [Route("{quizId}/items/{itemId}/answers/{userId}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<object> SaveAnswer(
            int quizId,
            int itemId,
            int userId,
            QuizItemUserAnswerDto dto,
            LinkGenerator linkGenerator
        )
        {
            _service.SaveUserAnswerForQuiz(quizId, userId, itemId, dto.Answer ?? "");
            return Created(
                linkGenerator.GetUriByAction(HttpContext, nameof(GetQuizFeedback), null,
                    new { quizId = quizId, userId = 1 }),
                new
                {
                    answer = dto.Answer,
                });
        }

        [Route("{quizId}/answers/{userId}")]
        [HttpGet]
        public ActionResult<object> GetQuizFeedback(int quizId, int userId)
        {
            var feedback = _service.GetUserAnswersForQuiz(quizId, userId);
            return new FeedbackDto
            {
                QuizId = quizId,
                UserId = userId,
                TotalQuestions = _service.FindQuizById(quizId)?.Items.Count ?? 0,
                Answers = feedback.Select(a =>
                    new
                    {
                        question = a.QuizItem.Question,
                        answer = a.Answer,
                        isCorrect = a.IsCorrect()
                    }
                ).AsEnumerable()
            };
        }
    }
}