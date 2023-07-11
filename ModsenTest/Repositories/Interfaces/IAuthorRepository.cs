using ModsenTest.Models;

namespace ModsenTest.Repositories.Interfaces;

public interface IAuthorRepository
{
    public Task<Author?> GetAuthorByNameAsync(string firstName, string lastName);
    public Task<Author> CreateAuthorAsync(Author author);
    public Task<Author?> GetAuthorByIdAsync(Guid authorId);
}