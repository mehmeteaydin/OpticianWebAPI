using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticianWebAPI.Models
{
    public class Glasses
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        // --- Frame İlişkisi ---
        [Required]
        [Column("frame_id")]
        public Guid FrameId { get; set; }
        [ForeignKey("FrameId")]
        public Frame? Frame { get; set; }
        // --- Lens (Cam) İlişkisi ---
        [Required]
        [Column("lens_id")]
        public Guid LensId { get; set; }
        [ForeignKey("LensId")]
        public Lens? Lens { get; set; }
        [Required]
        [Column("glasses_type")]
        public GlassesType Type { get; set; }
        [Column("total_price", TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; private set; }
        [MaxLength(200)]
        [Column("description")]
        public string? Description { get; set; }
        [Column("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }  

        public void CalculatePrice()
        {
            if (Frame == null || Lens == null)
            {
                throw new InvalidOperationException("Fiyat hesaplanabilmesi için Frame ve Lens nesnelerinin yüklü olması gerekir.");
            }
            decimal baseCost = Frame.Cost + Lens.Cost;
            TotalPrice = baseCost * 1.10m; 
        } 
    }

    public enum GlassesType
    {
        SingleVision = 1,
        Bifocal = 2,
        Progressive = 3,
        Reading = 4,
        Sunglasses = 5,
        BlueLightFilter = 6
    }
}