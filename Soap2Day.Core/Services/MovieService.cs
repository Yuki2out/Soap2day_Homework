using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using Soap2Day.Core.Models;    
using Soap2Day.Core.Contracts;
using Soap2Day.Infrastructure.Data;

namespace Soap2Day.Core.Services
{
    public class MovieService : IMovieService 
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

        // МЕТОДЪТ ЗА ТЪРСЕНЕ
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
                Title = dto.Title ?? "Unknown Title", 
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