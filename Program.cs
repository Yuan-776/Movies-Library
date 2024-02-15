class Program
{
    static void Main(string[] args)
    {
        var movieRepository = new MovieRepository();

        while (true)
        {
            Console.WriteLine("\nSelect an operation:");
            Console.WriteLine("1 - View all movies");
            Console.WriteLine("2 - Add a new movie");
            Console.WriteLine("3 - Exit");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    movieRepository.DisplayMovies();
                    break;
                case "2":
                    Console.WriteLine("Enter movie ID:");
                    var movieId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter movie title:");
                    var title = Console.ReadLine();
                    Console.WriteLine("Enter genres (separated by '|'):");
                    var genres = Console.ReadLine();

                    var movieToAdd = new Movie { MovieId = movieId, Title = title, Genres = genres };
                    if (movieRepository.AddMovie(movieToAdd))
                    {
                        Console.WriteLine("The movie has been successfully added.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add the movie. Check the log for more information.");
                    }
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option, please select again.");
                    break;
            }
        }
    }
}
