using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticianWebAPI.Models
{
    [Table("lenses")]
    public class Lens
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Marka (Brand) alanı zorunludur.")]
        [MaxLength(50, ErrorMessage = "Marka en fazla 50 karakter olabilir.")]
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [Column("degree_left")] 
        [Range(-30.0, 30.0, ErrorMessage = "Sol göz derecesi geçerli aralıkta olmalıdır.")]
        public double Left { get; set; }
        [Required]
        [Column("degree_right")]
        [Range(-30.0, 30.0, ErrorMessage = "Sağ göz derecesi geçerli aralıkta olmalıdır.")]
        public double Right { get; set; }
        [Required]
        [Column("cost", TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Maliyet 0'dan küçük olamaz.")]
        public decimal Cost { get; set; }
        [Column("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("updatedAt")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}