using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace PersonalLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }
    }
}
