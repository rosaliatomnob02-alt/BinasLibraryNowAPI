using BinasLibraryNowAPI.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BinasLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Static list to persist data in memory while the app is running
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Shadows of Tomorrow",
                Author = "Luna Reyes",
                Genre = "Science Fiction",
                isAvailable = true,
                PublishedYear = 2021
            },
            new Book
            {
                Id = 2,
                Title = "Whispers in the Wind",
                Author = "Marco Dela Cruz",
                Genre = "Mystery",
                isAvailable = true,
                PublishedYear = 2018
            }
        };

        // GET: api/v1/books
        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(books);
        }

        // GET: api/v1/books/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound(new { message = "Book not found" }) : Ok(book);
        }

        // POST: api/v1/books
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
            books.Add(newBook);
            // Returns a 201 Created status with the location of the new resource
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        // PUT: api/v1/books/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.isAvailable = updatedBook.isAvailable;
            book.PublishedYear = updatedBook.PublishedYear;

            return Ok(book);
        }

        // DELETE: api/v1/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { status = "error", message = "Book not found" });
            }

            books.Remove(book);
            return Ok(new { status = "success", message = "Book deleted successfully" });
        }
    }
}
