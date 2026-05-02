using BinasLibraryNowAPI.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BinasLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
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
            {
                new Book
                {
                 Id = 2,
                 Title = "Whispers in the Wind",
                 Author = "Marco Dela Cruz",
                 Genre = "Mystery",
                 isAvailable = true,
                 PublishedYear = 2018
                }
            }
        };

        [HttpGet]


        public IActionResult GetAll() => Ok(books);


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult POST([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return Ok(newBook);
        }

        [HttpPut("{id}")]
        public IActionResult PUT(int id , [FromBody] Book updatedBook)
        {
           var book = books.FirstOrDefault (b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }

            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.isAvailable = updatedBook.isAvailable;
            book.PublishedYear = updatedBook.PublishedYear;
            
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult DELETE(int id)
        {
            var book = books.FirstOrDefault( b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }

            books.Remove(book);
            return Ok(book);

        }
    }
}

