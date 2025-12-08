using System;
using System.ComponentModel.DataAnnotations;

namespace OpticianWebAPI.DTOs
{
    public class CreateFrameRequest
    {
        [Required(ErrorMessage = "Marka (Brand) alanı zorunludur.")]
        [MaxLength(30, ErrorMessage = "Marka alanı en fazla 30 karakter olabilir.")]
        public string Brand { get; set; } = string.Empty;
        [Required(ErrorMessage = "Model kodu (ModelCode) zorunludur.")]
        [MaxLength(30, ErrorMessage = "Model kodu en fazla 30 karakter olabilir.")]
        public string ModelCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Maliyet (Cost) alanı zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maliyet 0'dan küçük olamaz.")]
        public decimal Cost { get; set; }
        [MaxLength(30, ErrorMessage = "Renk alanı en fazla 30 karakter olabilir.")]
        public string? Color { get; set; }
        [MaxLength(20, ErrorMessage = "Materyal alanı en fazla 20 karakter olabilir.")]
        public string? Material { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
        public int StockQuantity { get; set; }
    }
}