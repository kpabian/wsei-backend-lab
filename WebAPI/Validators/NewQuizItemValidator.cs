using FluentValidation;
using WebAPI.Dto;

namespace WebAPI.Validators
{
    public class NewQuizItemValidator : AbstractValidator<NewQuizItemDto>
    {
        public NewQuizItemValidator()
        {
            RuleFor(q => q.Qestion)
                .MaximumLength(200).WithMessage("Pytanie nie może być dłuższe niż 200 znaków.")
                .MinimumLength(3).WithMessage("Pytanie nie może być krótsze od 3 znaków!");
            RuleForEach(q => q.Options)
                .MaximumLength(200)
                .MinimumLength(1);
            RuleFor(q => new { q.Options, q.CorrectOptionIndex })
                .Must(t => t.Options.Count > t.CorrectOptionIndex)
                .WithMessage("Indeks poprawnej odpowiedzi nie może być większy niż liczba odpowiedzi!");
        }
    }
}
