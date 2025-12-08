using System;
using System.ComponentModel.DataAnnotations;

namespace OpticianWebAPI.DTOs
{
    public class UpdateLensRequest
    {
        [Required(ErrorMessage = "Marka (Brand) alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Marka en fazla 50 karakter olabilir.")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sol göz derecesi zorunludur.")]
        [Range(-30.0, 30.0, ErrorMessage = "Sol göz derecesi -30 ile +30 arasında olmalıdır.")]
        public double Left { get; set; }

        [Required(ErrorMessage = "Sağ göz derecesi zorunludur.")]
        [Range(-30.0, 30.0, ErrorMessage = "Sağ göz derecesi -30 ile +30 arasında olmalıdır.")]
        public double Right { get; set; }

        [Required(ErrorMessage = "Maliyet bilgisi zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maliyet 0'dan küçük olamaz.")]
        public decimal Cost { get; set; }
    }
}