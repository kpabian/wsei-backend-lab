﻿namespace BackendLab01;

public interface IQuizUserService
{
    Quiz CreateAndGetQuizRandom(int count);

    Quiz? FindQuizById(int id);

    List<Quiz> GetAllQuizes();

    void SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer);

    List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId);

    int CountCorrectAnswersForQuizFilledByUser(int quizId, int userId)
    {
        return GetUserAnswersForQuiz(quizId, userId)
            .Count(e => e.IsCorrect());
    }
}