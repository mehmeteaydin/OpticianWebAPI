using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpticianWebAPI.Models
{
    public class Frame
    {
        [Key]
        [Column("id")]
        public Guid Id{get;set;}
        [Required]
        [Column("brand")]
        [MaxLength(30)]
        public string? Brand {get;set;}
        [Required]
        [Column("modelCode")]
        [MaxLength(30)]
        public string? ModelCode {get;set;}
        [MaxLength(30)]
        [Column("color")]
        public string? Color { get; set; }
        [MaxLength(20)]
        [Column("material")]
        public string? Material { get; set; }
        [Column("quantity")]
        public int StockQuantity { get; set; }
    }
}