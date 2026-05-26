using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        // --- 1. Основні дані про книгу ---
        [Required(ErrorMessage = "Назва обов'язкова")]
        [Display(Name = "Назва книги")]
        public string Title { get; set; }

        [Display(Name = "Автор(и)")]
        public string Author { get; set; }

        [Display(Name = "Видавництво")]
        public string Publisher { get; set; }

        [Display(Name = "Рік видання")]
        public int? Year { get; set; }

        [Display(Name = "Кількість сторінок")]
        public int? PageCount { get; set; }

        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Тип обкладинки")]
        public string CoverType { get; set; }

        // --- 2. Категоризація ---
        [Display(Name = "Розділ бібліотеки")]
        public string Section { get; set; }

        [Display(Name = "Теги (через кому)")]
        public string Tags { get; set; }

        // --- 3. Походження та статус ---
        [Display(Name = "Джерело появи")]
        public string Origin { get; set; }

        [Display(Name = "Дата додавання в бібліотеку")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Display(Name = "Поточна наявність")]
        public string AvailabilityStatus { get; set; } = "Вдома"; // Варіанти: Вдома, Видана на руки, Втрачена, Архів

        // --- 4. Особиста оцінка ---
        [Display(Name = "Статус прочитання")]
        public string ReadStatus { get; set; }

        [Display(Name = "Особиста оцінка (1-10)")]
        [Range(1, 10, ErrorMessage = "Оцінка має бути від 1 до 10")]
        public int? PersonalRating { get; set; }

        [Display(Name = "Відгук / Історія прочитання")]
        public string Review { get; set; }

        // --- 5. Розширені дані (Боржники) ---
        [Display(Name = "Кому видана (Ім'я)")]
        public string BorrowerName { get; set; }

        [Display(Name = "Контакти боржника")]
        public string BorrowerContact { get; set; }

        [Display(Name = "Дата видачі")]
        public DateTime? BorrowDate { get; set; }

        [Display(Name = "Очікувана дата повернення")]
        public DateTime? ExpectedReturnDate { get; set; }

        // --- 6. Wishlist (Список бажаного) ---
        public bool IsWishlist { get; set; } = false;

        [Display(Name = "Посилання на магазин")]
        public string ShopLink { get; set; }
    }
}