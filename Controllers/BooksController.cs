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

        public IActionResult Index(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                books = books.Where(b => b.Title.ToLower().Contains(searchString) ||
                                         b.Author.ToLower().Contains(searchString)).ToList();
            }

            return View(books);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Inventory()
        {
            var books = _bookService.GetAllBooks();

            

            ViewBag.TotalBooks = books.Count;
            ViewBag.ReadBooks = books.Count(b => b.ReadStatus == "Прочитано");
            ViewBag.InProcessBooks = books.Count(b => b.ReadStatus == "В процесі");
            ViewBag.PlannedBooks = books.Count(b => b.ReadStatus == "В планах");
            ViewBag.BorrowedBooks = books.Count(b => b.AvailabilityStatus == "Видана на руки");



            var sectionStats = books
                .GroupBy(b => b.Section)
                .Select(g => new KeyValuePair<string, int>(string.IsNullOrEmpty(g.Key) ? "Без розділу" : g.Key, g.Count()))
                .ToList();

            return View(sectionStats);
        }

    }
}