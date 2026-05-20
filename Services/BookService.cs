using System.Text.Json;
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
    }
}