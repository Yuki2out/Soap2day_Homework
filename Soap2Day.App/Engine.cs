using Spectre.Console;
using Soap2Day.Core.Contracts; 
using Soap2Day.Core.Services;  
using Soap2Day.Core.Models;    
using Soap2Day.App.Menu;       
using Soap2Day.Infrastructure.Data;

namespace Soap2Day.App
{
    public class Engine
    {
        
        private readonly IMovieService _service = new MovieService(); 
        private bool isRunning;

        public Engine()
        {
            
            using (var context = new Soap2DayDbContext())
            {
                // Изтрива базата, ако е стара/грешна (само за разработка!)
                // context.Database.EnsureDeleted(); 
                
                // Създава базата наново с правилните типове (Genre като INT)
                context.Database.EnsureCreated();
            }
            
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

                    // 1. Избор на Жанр от списък (Enum Selection)
                    var genre = AnsiConsole.Prompt(
                        new SelectionPrompt<Genre>()
                            .Title("Изберете [green]жанр[/]:")
                            .AddChoices(Enum.GetValues<Genre>()));

                    // 2. Валидация на Рейтинг (Validation)
                    var rating = AnsiConsole.Prompt(
                        new TextPrompt<double>("Рейтинг (0-10):")
                            .Validate(r => 
                            {
                                return r switch
                                {
                                    < 0 => ValidationResult.Error("[red]Рейтингът не може да е под 0[/]"),
                                    > 10 => ValidationResult.Error("[red]Рейтингът не може да е над 10[/]"),
                                    _ => ValidationResult.Success(),
                                };
                            }));

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
                    var foundMovies = _service.SearchMovies(searchTerm);

                    if (foundMovies.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]❌ Не бяха намерени филми.[/]");
                    }
                    else
                    {
                        
                        MenuRenderer.RenderTable(foundMovies, $"Резултати за: {searchTerm}");
                    }
                    break;

               case "Списък с филми":
                    var allMovies = _service.GetAllMovies();
                    if (!allMovies.Any())
                    {
                        AnsiConsole.MarkupLine("[yellow]⚠ Базата данни е празна. Добавете филм първо![/]");
                    }
                    else
                    {
                        MenuRenderer.RenderTable(allMovies, "Всички филми в Soap2Day");
                    }
                    break;

                case "Изтрий филм":
                    var movieToDelete = AnsiConsole.Ask<string>("Въведете заглавие:");
                    _service.DeleteMovie(movieToDelete);
                    AnsiConsole.MarkupLine($"[red]🗑 Премахнато![/]");
                    break;

                case "Изход":
                    isRunning = false;
                    break;
            }
        }
    }
}