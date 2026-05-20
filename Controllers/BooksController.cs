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

       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.AddBook(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id); 
            return RedirectToAction("Index"); 
        }
    }
}