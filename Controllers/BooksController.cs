using System;
using System.Linq;
using System.Collections.Generic;
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

        public IActionResult Index(string searchString, string sectionFilter, string statusFilter, bool isWishlist = false)
        {
            var books = _bookService.GetAllBooks();
            var query = books.AsEnumerable();

           
            query = query.Where(b => b.IsWishlist == isWishlist);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    (!string.IsNullOrEmpty(b.Title) && b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(b.Author) && b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(b.Tags) && b.Tags.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrEmpty(sectionFilter))
                query = query.Where(b => b.Section == sectionFilter);

            if (!string.IsNullOrEmpty(statusFilter))
                query = query.Where(b => b.ReadStatus == statusFilter);

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentSection = sectionFilter;
            ViewBag.CurrentStatus = statusFilter;
            ViewBag.IsWishlist = isWishlist;

            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null) return NotFound();
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
                
                if (book.IsWishlist)
                {
                    book.AvailabilityStatus = "Відсутня";
                    book.ReadStatus = "В планах";
                }
                else
                {
                    book.AvailabilityStatus = "Вдома";
                }

                _bookService.AddBook(book);

                
                if (book.IsWishlist)
                    return RedirectToAction("Index", new { isWishlist = true });

                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null) return NotFound();
            return View("~/Views/Books/Edit.cshtml", book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(book);

               
                if (book.IsWishlist)
                    return RedirectToAction("Index", new { isWishlist = true });

                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _bookService.GetBookById(id);
            bool wasWishlist = book != null && book.IsWishlist;

            _bookService.DeleteBook(id);

            
            return RedirectToAction("Index", new { isWishlist = wasWishlist });
        }

       

        [HttpPost]
        public IActionResult IssueBook(int Id, string BorrowerName, string BorrowerContact, DateTime BorrowDate, DateTime ExpectedReturnDate)
        {
            var book = _bookService.GetBookById(Id);
            if (book != null)
            {
                book.AvailabilityStatus = "Видана на руки";
                book.BorrowerName = BorrowerName;
                book.BorrowerContact = BorrowerContact;
                book.BorrowDate = BorrowDate;
                book.ExpectedReturnDate = ExpectedReturnDate;

                _bookService.UpdateBook(book);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ReturnBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book != null)
            {
                book.AvailabilityStatus = "Вдома";
               
                book.BorrowerName = null;
                book.BorrowerContact = null;
                book.BorrowDate = null;
                book.ExpectedReturnDate = null;

                _bookService.UpdateBook(book);
            }
            return RedirectToAction("Index");
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