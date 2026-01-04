using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soap2Day.Data;
using Soap2Day.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soap2Day.Services
{
    public class MovieService
    {
        // Метод за взимане на всички филми
        public List<MovieDto> GetAllMovies()
        {
            using var context = new Soap2DayDbContext();
            return context.Movies
                .Select(m => new MovieDto {
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre,
                    Rating = m.Rating
                }).ToList();
        }

        // МЕТОДЪТ ЗА ТЪРСЕНЕ, КОЙТО ЛИПСВАШЕ:
        public List<MovieDto> SearchMovies(string searchTerm)
        {
            using var context = new Soap2DayDbContext();
            return context.Movies
                .Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(m => new MovieDto {
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre,
                    Rating = m.Rating
                }).ToList();
        }

        // Метод за добавяне
        public void AddMovie(MovieDto dto)
        {
            using var context = new Soap2DayDbContext();
            var movie = new Movie { 
                Title = dto.Title, 
                Year = dto.Year, 
                Genre = dto.Genre, 
                Rating = dto.Rating 
            };
            context.Movies.Add(movie);
            context.SaveChanges();
        }
        
        // Метод за изтриване
        public void DeleteMovie(string title)
        {
            using var context = new Soap2DayDbContext();
            var movie = context.Movies.FirstOrDefault(m => m.Title == title);
            if (movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
        }
    }
}