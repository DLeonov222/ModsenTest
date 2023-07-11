using ModsenTest.Models;

namespace ModsenTest.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(Guid id);

    Task<Book> CreateBookAsync(Book newBook);

    Task<Book> UpdateBookAsync(Book bookToUpdate, Book updatedBook);

    Task<Book> DeleteBookAsync(Book bookToDelete);
}