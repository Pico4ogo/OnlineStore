using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Entities
{
    public class Film
    {
        [HiddenInput(DisplayValue=false)]
        public int FilmId { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage="Пожалуйста, введите название фильма")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name="Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для фильма")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для фильма")]
        public string Category { get; set; }

        [Display(Name = "Цена (грн)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
