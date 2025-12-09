using System;
using System.ComponentModel.DataAnnotations;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class UpdateGlassesRequest
    {
        [Required(ErrorMessage = "Çerçeve (Frame) seçimi zorunludur.")]
        public Guid FrameId { get; set; }
        [Required(ErrorMessage = "Cam (Lens) seçimi zorunludur.")]
        public Guid LensId { get; set; }
        [Required(ErrorMessage = "Gözlük tipi seçimi zorunludur.")]
        [EnumDataType(typeof(GlassesType))]
        public GlassesType Type { get; set; }
        [MaxLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string? Description { get; set; }
    }
}