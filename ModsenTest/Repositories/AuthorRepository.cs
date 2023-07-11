using Microsoft.EntityFrameworkCore;
using ModsenTest.Data;
using ModsenTest.Models;
using ModsenTest.Repositories.Interfaces;

namespace ModsenTest.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _context;

    public AuthorRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Author?> GetAuthorByNameAsync(string firstName, string lastName)
    {
        return await _context.Authors.FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName);
    }

    public async Task<Author> CreateAuthorAsync(Author author)
    {
        var created = (await _context.Authors.AddAsync(author)).Entity;
        await _context.SaveChangesAsync();
        return created;
    }

    public Task<Author?> GetAuthorByIdAsync(Guid authorId)
    {
        return _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);
    }
}