using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Soap2Day.Infrastructure.Common;

namespace Soap2Day.Infrastructure.Data
{
    public class Soap2DayDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConstants.ConnectionString);
            
        }
    }
}