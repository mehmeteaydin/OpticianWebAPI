using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticianWebAPI.Models
{
    [Table("frames")]
    public class Frame
    {
        [Key]
        [Column("id")]
        public Guid Id{get;set;}
        [Required]
        [Column("brand")]
        [MaxLength(30)]
        public string Brand {get;set;} = string.Empty;
        [Required]
        [Column("modelCode")]
        [MaxLength(30)]
        public string ModelCode {get;set;} = string.Empty;
        
        [Column("cost")]
        public decimal Cost{get;set;}
        [MaxLength(30)]
        [Column("color")]
        public string? Color { get; set; }
        [MaxLength(20)]
        [Column("material")]
        public string? Material { get; set; }
        [Column("quantity")]
        public int StockQuantity { get; set; }
        [Column("createdAt")]
        public DateTimeOffset CreatedAt {get;set;}
        [Column("updatedAt")]
        public DateTimeOffset? UpdatedAt {get;set;}
    }
}