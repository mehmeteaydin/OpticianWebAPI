using System;
using System.ComponentModel.DataAnnotations;

namespace OpticianWebAPI.DTOs
{
    public class UpdateFrameRequest
    {
        [Required]
        [MaxLength(30)]
        public string? Brand { get; init; }

        [Required]
        [MaxLength(30)]
        public string? ModelCode { get; init; }

        [MaxLength(30)]
        public string? Color { get; init; }

        [MaxLength(20)]
        public string? Material { get; init; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; init; }
    }
}