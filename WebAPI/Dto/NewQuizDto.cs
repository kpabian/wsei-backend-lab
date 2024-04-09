using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto
{
    public class NewQuizDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
    }
}
