using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть назву книги")]
        [Display(Name = "Назва")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введіть автора")]
        [Display(Name = "Автор(и)")]
        public string Author { get; set; }

        [Display(Name = "Видавництво")]
        public string? Publisher { get; set; }

        [Display(Name = "Рік видання")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Оберіть розділ бібліотеки")]
        [Display(Name = "Розділ (наприклад: Фантастика, Хобі)")]
        public string Section { get; set; }

        [Display(Name = "Походження (звідки взялася)")]
        public string? Origin { get; set; }

        [Display(Name = "В наявності")]
        public bool IsAvailable { get; set; } = true;

        [Range(1, 10, ErrorMessage = "Оцінка має бути від 1 до 10")]
        [Display(Name = "Оцінка (1-10)")]
        public int? PersonalRating { get; set; }
    }
}