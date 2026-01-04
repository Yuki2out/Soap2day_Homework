using System.ComponentModel.DataAnnotations;


namespace Soap2Day.Data
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        public int Year { get; set; }

        public string Genre { get; set; } = null!;

        [Range(0, 10)]
        public double Rating { get; set; } 
    }
}