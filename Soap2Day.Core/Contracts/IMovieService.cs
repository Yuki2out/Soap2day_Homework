using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soap2Day.Core.Models; 
using System.Collections.Generic;

namespace Soap2Day.Core.Contracts
{
    public interface IMovieService 
    {
        List<MovieDto> GetAllMovies();
        void AddMovie(MovieDto movie);
        List<MovieDto> SearchMovies(string term);
        void DeleteMovie(string title);
    }
}