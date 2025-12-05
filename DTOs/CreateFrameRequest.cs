using System;
using System.ComponentModel.DataAnnotations;

namespace OpticianWebAPI.DTOs
{
    public class CreateFrameRequest
    {
        [Required(ErrorMessage = "Marka boş bırakılamaz.")]
        [MaxLength(30)]
        public string? Brand { get; init; }

        [Required(ErrorMessage = "Model kodu zorunludur.")]
        [MaxLength(30)]
        public string? ModelCode { get; init; }

        [MaxLength(30)]
        public string? Color { get; init; }

        [MaxLength(20)]
        public string? Material { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok adedi negatif olamaz.")]
        public int StockQuantity { get; init; }
    }
}