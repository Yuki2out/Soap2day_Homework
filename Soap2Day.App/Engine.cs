using Spectre.Console;
using Soap2Day.Services;
using Soap2Day.Data;
using Soap2Day.Models;

namespace Soap2Day.App
{
    public class Engine
    {
        private readonly MovieService _service = new MovieService();
        private bool isRunning;

        public Engine()
        {
            this.isRunning = true;
        }

        public void Run()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Soap2Day").Centered().Color(Color.Cyan1));
            
            while (isRunning)
            {
                try
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Главно Меню[/]")
                            .PageSize(10)
                            .AddChoices(new[] {
                                "Списък с филми", 
                                "Добави филм", 
                                "Търсене", 
                                "Изтрий филм", 
                                "Изход"
                            }));

                    ProcessCommand(choice);
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Грешка: {ex.Message}[/]");
                    AnsiConsole.WriteLine("Натиснете клавиш за продължение...");
                    Console.ReadKey();
                }
            }
        }

        private void ProcessCommand(string command)
        {
            switch (command)
            {
                case "Добави филм":
                    var title = AnsiConsole.Ask<string>("Заглавие:");
                    var year = AnsiConsole.Ask<int>("Година:");
                    var genre = AnsiConsole.Ask<string>("Жанр:");
                    var rating = AnsiConsole.Ask<double>("Рейтинг (0-10):");

                    _service.AddMovie(new MovieDto { 
                        Title = title, 
                        Year = year, 
                        Genre = genre, 
                        Rating = rating 
                    });
                    AnsiConsole.MarkupLine("[green]✔ Филмът е добавен успешно![/]");
                    break;

                case "Търсене":
                    var searchTerm = AnsiConsole.Ask<string>("[yellow]Въведете име на филм:[/]");
                    var foundMovies = _service.SearchMovies(searchTerm); // Поправено от _movieService

                    if (foundMovies.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]❌ Не бяха намерени филми.[/]");
                    }
                    else
                    {
                        RenderTable(foundMovies, $"Резултати за: {searchTerm}");
                    }
                    break;

                case "Списък с филми":
                    var allMovies = _service.GetAllMovies();
                    RenderTable(allMovies, "Всички филми в Soap2Day");
                    break;

                case "Изтрий филм":
                    var movieToDelete = AnsiConsole.Ask<string>("Въведете точното заглавие на филма за изтриване:");
                    _service.DeleteMovie(movieToDelete);
                    AnsiConsole.MarkupLine($"[red]🗑 Филмът '{movieToDelete}' бе премахнат (ако е съществувал).[/]");
                    break;

                case "Изход":
                    isRunning = false;
                    
                    break;
            }
        }

        // Помощен метод за рендване на таблица (DRY - Don't Repeat Yourself)
        private void RenderTable(List<MovieDto> movies, string title)
        {
            var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Cyan1);
            table.Title($"[bold yellow]{title}[/]");
            
            table.AddColumn("Заглавие");
            table.AddColumn("Година");
            table.AddColumn("Жанр");
            table.AddColumn("Рейтинг");

            foreach (var m in movies)
            {
                table.AddRow(m.Title, m.Year.ToString(), m.Genre, $"⭐ {m.Rating:F1}");
            }

            AnsiConsole.Write(table);
        }
    }
}