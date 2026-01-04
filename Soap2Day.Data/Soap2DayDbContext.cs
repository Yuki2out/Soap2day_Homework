using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Soap2Day.Data
{
    public class Soap2DayDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // Използваме порта и паролата от Docker командата
    optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Soap2DayDb;User Id=sa;Password=Soap2dayy@a;TrustServerCertificate=True;");
}
    }
}