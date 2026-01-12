using System.ComponentModel.DataAnnotations;

namespace Soap2Day.Infrastructure.Data
{
    public class Movie 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        public int Year { get; set; }

        
        public Genre Genre { get; set; } 

        [Range(0, 10)]
        public double Rating { get; set; } 
    }
}