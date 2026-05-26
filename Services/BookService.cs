using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Models;

namespace PersonalLibrary.Services
{
    public class BookService
    {
        
        private readonly string _filePath = "books.json";

        
        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Book>(); 
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        
        public void SaveBooks(List<Book> books)
        {
            var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        
        public void AddBook(Book newBook)
        {
            var books = GetAllBooks();

            
            newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;

            books.Add(newBook);
            SaveBooks(books);
        }

        public void DeleteBook(int id)
        {
            var books = GetAllBooks(); 
            var bookToRemove = books.FirstOrDefault(b => b.Id == id); 

            if (bookToRemove != null)
            {
                books.Remove(bookToRemove); 
                SaveBooks(books); 
            }
        }


       
        public Book GetBookById(int id)
        {
            var books = GetAllBooks();
            return books.FirstOrDefault(b => b.Id == id);
        }

        
        public void UpdateBook(Book updatedBook)
        {
            var books = GetAllBooks();
            var index = books.FindIndex(b => b.Id == updatedBook.Id);

            if (index != -1) 
            {
                books[index] = updatedBook; 
                SaveBooks(books); 
            }
        }


        
       
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(); 
            }
            return View(book);
        }

       
        
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(book); 
                return RedirectToAction("Index"); 
            }

            
            return View(book);
        }
    }
}