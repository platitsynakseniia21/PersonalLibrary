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

        public void AddBook(Book book)
        {
            var books = GetAllBooks();
            book.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(book);
            SaveBooks(books);
        }

        public void DeleteBook(int id)
        {
            var books = GetAllBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
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
    }
}