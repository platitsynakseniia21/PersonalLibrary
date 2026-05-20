using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace PersonalLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        // Конструктор
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // 1. Метод для главной страницы (список книг)
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        // 2. Метод, который просто открывает пустую форму
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Метод, который сохраняет данные после нажатия кнопки "Зберегти"
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
    }
}