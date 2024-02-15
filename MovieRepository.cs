using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MovieRepository
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private List<Movie> movies;
    private readonly string filePath = @"ml-latest-small/movies.csv";

    public MovieRepository()
    {
        movies = LoadMovies();
    }

    private List<Movie> LoadMovies()
    {
        try
        {
            return File.ReadAllLines(filePath)
                .Skip(1)
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Movie
                    {
                        MovieId = int.Parse(parts[0]),
                        Title = parts[1].Trim('"'), 
                        Genres = parts[2]
                    };
                }).ToList();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred while reading movie data.");
            return new List<Movie>();
        }
    }

    public bool AddMovie(Movie newMovie)
    {
        if (movies.Any(m => m.MovieId == newMovie.MovieId))
        {
            logger.Error($"Movie ID {newMovie.MovieId} already exists.");
            return false;
        }

        if (movies.Any(m => m.Title.Equals(newMovie.Title, StringComparison.OrdinalIgnoreCase)))
        {
            logger.Error($"Movie title \"{newMovie.Title}\" already exists.");
            return false;
        }

        movies.Add(newMovie);

        try
        {
            using (var sw = File.AppendText(filePath))
            {
                sw.WriteLine($"{newMovie.MovieId},{newMovie.Title},{newMovie.Genres}");
            }
            logger.Info($"Movie \"{newMovie.Title}\" has been successfully added.");
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error occurred while adding a new movie to the file.");
            return false;
        }
    }

    public void DisplayMovies()
    {
        foreach (var movie in movies)
        {
            Console.WriteLine($"{movie.MovieId}: {movie.Title} - {movie.Genres}");
        }
    }
}
