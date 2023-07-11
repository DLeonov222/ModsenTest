using Microsoft.EntityFrameworkCore;
using ModsenTest.Data;
using ModsenTest.Models;
using ModsenTest.Repositories.Interfaces;

namespace ModsenTest.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly DataContext _context;

    public GenreRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Genre?> GetGenreByNameAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(x => x.GenreName == name);
    }

    public async Task<Genre> CreateGenreAsync(Genre genre)
    {
        return (await _context.Genres.AddAsync(genre)).Entity;
    }
}