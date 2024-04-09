using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace WebAPI
{
    public static class SeedData
    {
        public static void Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();

            var quiz1Items = new List<QuizItem>
        {
            new QuizItem(1, "What is the capital of France?", new List<string> {"London", "Berlin", "Madrid" }, "Paris"),
            new QuizItem(2, "What is the capital of Germany?", new List<string> {"Paris", "London", "Madrid" }, "Berlin"),
            new QuizItem(3, "What is the capital of Spain?", new List<string> {"Paris", "Berlin", "London" }, "Madrid")
        };

            var quiz2Items = new List<QuizItem>
        {
            new QuizItem(4, "What's the formula for water?", new List<string> {"CO2", "O2" }, "H2O"),
            new QuizItem(5, "What's the formula for carbon dioxide?", new List<string> {"H2O", "O2" }, "CO2"),
            new QuizItem(6, "What's the formula for oxygen?", new List<string> {"H2O", "CO2" }, "O2")
        };

            quiz1Items.ForEach(e => quizItemRepo.Add(e));
            quiz2Items.ForEach(e => quizItemRepo.Add(e));

            var quiz1 = new Quiz { Id = 1, Items = quiz1Items, Title = "Geography" };
            var quiz2 = new Quiz { Id = 2, Items = quiz2Items, Title = "Chemistry" };

            quizRepo.Add(quiz1);
            quizRepo.Add(quiz2);
        }
    }
}