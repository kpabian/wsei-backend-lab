using AutoMapper;
using BackendLab01;
using WebAPI.Dto;

public class AutoYapperProfiles : Profile
{
    public AutoYapperProfiles()
    {
        CreateMap<QuizItem, QuizItemDto>()
            .ForMember(
                q => q.Options,
                op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
        CreateMap<Quiz, QuizDto>()
            .ForMember(
                q => q.Items,
                op => op.MapFrom<List<QuizItem>>(i => i.Items)
            );
        CreateMap<NewQuizDto, Quiz>();
        CreateMap<FeedbackDto, QuizItemUserAnswer>();
    }
}