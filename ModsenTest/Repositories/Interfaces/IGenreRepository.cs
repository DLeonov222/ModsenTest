using ModsenTest.Models;

namespace ModsenTest.Repositories.Interfaces;

public interface IGenreRepository
{
    Task<Genre?> GetGenreByNameAsync(string name);

    Task<Genre> CreateGenreAsync(Genre genre);
}