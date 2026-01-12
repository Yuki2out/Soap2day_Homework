using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soap2Day.Core.Exceptions
{
    public class MovieNotFoundException : Exception {
        public MovieNotFoundException(int id) : base($"Movie with ID {id} was not found.") { }
        
    }
}