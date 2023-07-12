using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModsenTest.Data;
using ModsenTest.Dtos;
using ModsenTest.Models;
using ModsenTest.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ModsenTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("This controller is used to manage the books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns a list of all Books.", Description = "Returns a list of all books.")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<BookViewDto>),
            Description = "200 OK: Returns a list of all books.")]
        [SwaggerResponse(404, Description = "404 Not Found: No books were found in the database.")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Returns an book with the specified id.",
            Description = "Returns an book with the specified id")]
        [SwaggerResponse(200, Type = typeof(BookViewDto), Description = "200 OK: book with the specified id was found.")]
        [SwaggerResponse(404, Description = "404 Not Found: No book with the specified ID was found.")]
        public async Task<IActionResult> GetBookByIdAsync(Guid id)
        {
            var bookById = await _bookService.GetBookByIdAsync(id);
            return Ok(bookById);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Creates a new book.", Description = "Creates a new book.")]
        [SwaggerResponse(201, Type = typeof(CreatedBookDto), Description = "201 Created: book was created successfully.")]
        [SwaggerResponse(400, Description = "400 Bad Request: The request body is not valid.")]
        [SwaggerResponse(401, Description = "401 Unauthorized: Author is not authorized to create an book.")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDto createBookDto)
        {
            var createdBook = await _bookService.CreateBookAsync(createBookDto);
            return Created(new Uri($"{Request.Path}/{createdBook.Id}", UriKind.Relative), createdBook);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerOperation(Summary = "Updates an book with the specified id.",
            Description = "Updates an book with the specified id.")]
        [SwaggerResponse(200, Type = typeof(UpdatedBookDto), Description = "200 OK: book was updated successfully.")]
        [SwaggerResponse(400, Description = "400 Bad Request: The request body is not valid.")]
        [SwaggerResponse(401, Description = "401 Unauthorized: Author is not authorized to update the book.")]
        [SwaggerResponse(403, Description = "403 Forbidden: Author is not authorized to update the book.")]
        [SwaggerResponse(404, Description = "404 Not Found: No book with the specified ID was found.")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            return Ok(await _bookService.UpdateBookAsync(id, updateBookDto));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an book with the specified id.",
            Description = "Deletes an book with the specified id.")]
        [SwaggerResponse(200, Type = typeof(DeletedBookDto), Description = "200 OK: book was deleted successfully.")]
        [SwaggerResponse(401, Description = "401 Unauthorized: Author is not authorized to delete the book.")]
        [SwaggerResponse(403, Description = "403 Forbidden: Author is not authorized to delete the book.")]
        [SwaggerResponse(404, Description = "404 Not Found: No book with the specified ID was found.")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            return Ok(await _bookService.DeleteBookAsync(id));
        }
    }
}
