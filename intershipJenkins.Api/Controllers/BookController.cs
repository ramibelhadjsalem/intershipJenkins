using intershipJenkins.Api.DTOS;
using intershipJenkins.Api.Models;
using intershipJenkins.Api.Services.BookService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace intershipJenkins.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookServices _booksService;

        public BookController(IBookServices bookService)
        {
            _booksService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "1984",
                    Price = 9.99M,
                    Category = "Dystopian",
                    Author = "George Orwell",
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    DeletedAt = DateTime.UtcNow.AddYears(1),
                    Enabeled = true
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Price = 7.99M,
                    Category = "Fiction",
                    Author = "Harper Lee",
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    DeletedAt = DateTime.UtcNow.AddYears(1),
                    Enabeled = true
                },
                new Book
                {
                    Title = "The Great Gatsby",
                    Price = 10.99M,
                    Category = "Classic",
                    Author = "F. Scott Fitzgerald",
                    CreatedAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    DeletedAt = DateTime.UtcNow.AddYears(1),
                    Enabeled = true
                }
            };
            return Ok(books);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {

            var book = await _booksService.getById(id);

            if (book is  null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] BookDto book)
        {
           

            var newBook = new Book
            {
                Title = book.Title,
                Price = book.Price,
                Author = book.Author,
                Category = book.Category
            };
            await _booksService.createAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _booksService.getById(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

            await _booksService.updateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.getById(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.removeById(id);

            return NoContent();
        }
    }
}
