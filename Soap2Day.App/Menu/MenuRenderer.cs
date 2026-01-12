using Spectre.Console;
using Soap2Day.Core.Models;

namespace Soap2Day.App.Menu
{
    public static class MenuRenderer 
    {
        
        public static void RenderTable(List<MovieDto> movies, string title) 
        {
            var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Cyan1);
            table.Title($"[bold yellow]{title}[/]");
            
            table.AddColumn("Заглавие");
            table.AddColumn("Година");
            table.AddColumn("Жанр");
            table.AddColumn("Рейтинг");

            foreach (var m in movies)
            {
                
                table.AddRow(
                    m.Title ?? "Unknown", 
                    m.Year.ToString(), 
                    m.Genre.ToString(),
                    $"⭐ {m.Rating:F1}"
            );
            }

            AnsiConsole.Write(table);
        }
    }
}