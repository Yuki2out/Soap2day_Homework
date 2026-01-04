using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soap2Day.Models;

using System.ComponentModel.DataAnnotations;

namespace Soap2Day.Models
{
    public class MovieDto
    {   
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; } 
    }
}