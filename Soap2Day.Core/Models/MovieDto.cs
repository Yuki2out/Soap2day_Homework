using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Soap2Day.Core.Models;
using Soap2Day.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;


namespace Soap2Day.Core.Models
{
    public class MovieDto
    {
        [Required]
        public string? Title { get; set; } 
        [Required]
        public int Year { get; set; }
        [Required]
        public Genre Genre { get; set; } // Вече ще работи
        public double Rating { get; set; }
    }
}