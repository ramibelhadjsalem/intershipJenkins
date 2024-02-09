using System.ComponentModel.DataAnnotations;

namespace intershipJenkins.Api.DTOS
{
    public class BookDto
    {

        [Required]
        public string? Title { get; set; }

        [Required]

        public decimal Price { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(4)]
        public string? Category { get; set; }
        [Required]
        public string? Author { get; set; }
    }
}
