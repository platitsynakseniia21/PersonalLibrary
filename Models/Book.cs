using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва книги обов'язкова")]
        [Display(Name = "Назва книги")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Автор обов'язковий")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Видавництво")]
        public string? Publisher { get; set; }

        [Display(Name = "Рік видання")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Вкажіть розділ")]
        [Display(Name = "Розділ (Спеціальна, Белетристика тощо)")]
        public string Section { get; set; }

        [Display(Name = "В наявності вдома")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Особиста оцінка (1-10)")]
        [Range(1, 10, ErrorMessage = "Оцінка має бути від 1 до 10")]
        public int? PersonalRating { get; set; }

       

        [Display(Name = "Джерело появи (куплено, подаровано)")]
        public string? Origin { get; set; }

        [Display(Name = "Статус прочитання")]
        public string? ReadStatus { get; set; }

        [Display(Name = "Короткий відгук")]
        public string? Review { get; set; }

        [Display(Name = "Кому видана книга (Боржник)")]
        public string? BorrowerName { get; set; }
    }
}