using Microsoft.EntityFrameworkCore;
using ModsenTest.Data;
using ModsenTest.Models;
using ModsenTest.Repositories.Interfaces;

namespace ModsenTest.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DataContext _context;

    public BookRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.Include(e => e.Author).Include(e => e.Genre).ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _context.Books.Include(e => e.Author).Include(e => e.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Book> CreateBookAsync(Book newBook)
    {
        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();
        return _context.Entry(newBook).Entity;
    }

    public async Task<Book> UpdateBookAsync(Book bookToUpdate, Book updatedBook)
    {
        _context.Entry(bookToUpdate).CurrentValues.SetValues(updatedBook);
        await _context.SaveChangesAsync();
        return _context.Entry(bookToUpdate).Entity;
    }

    public async Task<Book> DeleteBookAsync(Book bookToDelete)
    {
        var deletedBook = _context.Books.Remove(bookToDelete).Entity;
        await _context.SaveChangesAsync();
        return deletedBook;
    }
}