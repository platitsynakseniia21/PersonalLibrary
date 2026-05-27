#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва книги є обов'язковою!")]
        [Display(Name = "Назва книги")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Вкажіть автора книги!")]
        [Display(Name = "Автор(и)")]
        public string Author { get; set; }

        [Display(Name = "Видавництво")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Вкажіть рік видання!")]
        [Display(Name = "Рік видання")]
        public int? Year { get; set; }

        [Display(Name = "Кількість сторінок")]
        public int? PageCount { get; set; }

        [Display(Name = "ISBN")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "Невірний формат ISBN (введіть 10 або 13 цифр, можна з дефісами)")]
        public string ISBN { get; set; }

        [Display(Name = "Тип обкладинки")]
        public string CoverType { get; set; }

        [Required(ErrorMessage = "Оберіть розділ бібліотеки!")]
        [Display(Name = "Розділ бібліотеки")]
        public string Section { get; set; }

        [Display(Name = "Теги (через кому)")]
        public string Tags { get; set; }

        [Display(Name = "Джерело появи")]
        public string Origin { get; set; }

        [Display(Name = "Дата додавання в бібліотеку")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Display(Name = "Поточна наявність")]
        public string AvailabilityStatus { get; set; } = "Вдома";

        [Display(Name = "Статус прочитання")]
        public string ReadStatus { get; set; }

        [Display(Name = "Особиста оцінка (1-10)")]
        [Range(1, 10, ErrorMessage = "Оцінка має бути від 1 до 10")]
        public int? PersonalRating { get; set; }

        [Display(Name = "Відгук / Історія прочитання")]
        public string Review { get; set; }

        [Display(Name = "Кому видана (Ім'я)")]
        public string BorrowerName { get; set; }

        [Display(Name = "Контакти боржника")]
        public string BorrowerContact { get; set; }

        [Display(Name = "Дата видачі")]
        public DateTime? BorrowDate { get; set; }

        [Display(Name = "Очікувана дата повернення")]
        public DateTime? ExpectedReturnDate { get; set; }

        public bool IsWishlist { get; set; } = false;

        [Display(Name = "Посилання на магазин")]
        public string ShopLink { get; set; }
    }
}