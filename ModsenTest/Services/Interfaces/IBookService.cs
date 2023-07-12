using ModsenTest.Dtos;

namespace ModsenTest.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewDto>> GetAllBooksAsync();

        Task<BookViewDto?> GetBookByIdAsync(Guid id);

        Task<CreatedBookDto> CreateBookAsync(CreateBookDto newCreateBook);

        Task<UpdatedBookDto> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto);

        Task<DeletedBookDto> DeleteBookAsync(Guid id);
    }
}
