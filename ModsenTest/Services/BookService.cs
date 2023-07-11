using AutoMapper;
using ModsenTest.Dtos;
using ModsenTest.Models;
using ModsenTest.Repositories.Interfaces;
using ModsenTest.Services.Interfaces;

namespace ModsenTest.Services
{
    public class BookService : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository,
            IAuthorRepository authorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<BookViewDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            if (!books.Any())
                throw new Exception("No Books found");

            return _mapper.Map<IEnumerable<BookViewDto>>(books);
        }

        public async Task<BookViewDto?> GetBookByIdAsync(Guid id)
        {
            var bookById = await _bookRepository.GetBookByIdAsync(id) ??
                            throw new KeyNotFoundException("Book with this id does not exist");
            return _mapper.Map<BookViewDto>(bookById);
        }

        public async Task<CreatedBookDto> CreateBookAsync(CreateBookDto newCreateBookDto)
        {
            var genre = await _genreRepository.GetGenreByNameAsync(newCreateBookDto.Genre) ??
                          await _genreRepository.CreateGenreAsync(new Genre
                          {
                             GenreName = newCreateBookDto.Genre
                          });

            var authorId =
                new Guid(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!);
            var author = await _authorRepository.GetAuthorByIdAsync(authorId) ??
                            throw new KeyNotFoundException("Author not found");

            var newBook = _mapper.Map<Book>(newCreateBookDto);
            newBook.Genre = genre;
            newBook.Author = author;

            return _mapper.Map<CreatedBookDto>(await _bookRepository.CreateBookAsync(newBook));
        }

        public async Task<UpdatedBookDto> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto)
        {
            var bookToUpdate = await _bookRepository.GetBookByIdAsync(id) ??
                                throw new KeyNotFoundException($"Book with id {id} not found");
            var currentAuthorId =
                new Guid(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!);

            if (bookToUpdate.AuthorId != currentAuthorId)
                throw new Exception("You are not allowed to update this Book");

            var updatedBook = _mapper.Map<Book>(updateBookDto);

            var updatedGenre = await _genreRepository.GetGenreByNameAsync(updateBookDto.Genre) ??
                                 await _genreRepository.CreateGenreAsync(new Genre
                                 {
                                     GenreName = updateBookDto.Genre
                                 });

            updatedBook.Id = id;
            updatedBook.GenreId = updatedGenre.Id;
            updatedBook.AuthorId = bookToUpdate.AuthorId;

            return _mapper.Map<UpdatedBookDto>(await _bookRepository.UpdateBookAsync(bookToUpdate, updatedBook));
        }

        public async Task<DeletedBookDto> DeleteBookAsync(Guid id)
        {
            var bookToDelete = await _bookRepository.GetBookByIdAsync(id) ??
                                throw new ArgumentException($"Book with id {id} not found");

            var currentAuthorId =
                new Guid(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value!);

            if (bookToDelete.AuthorId != currentAuthorId)
                throw new BadHttpRequestException("You are not allowed to delete this Book");

            return _mapper.Map<DeletedBookDto>(await _bookRepository.DeleteBookAsync(bookToDelete));
        }
    }
}
