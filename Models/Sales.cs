using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.Models
{
    [Table("sales")]
    public class Sales
    {
        [Key]
        [Column("id")]
        public Guid Id {get; set;}
        [Required(ErrorMessage = "Müşteri ismi boş bırakılamaz!")]
        [Column("customer_name")]
        public string CustomerName{get; set;} = string.Empty;
        [Required(ErrorMessage = "Müşteri telefon numarası boş bırakılamaz!")]
        [Column("customer_phone")]
        public string CustomerPhone{get; set;} = string.Empty;

        [Column("sale_date")]
        public DateTimeOffset SaleDate{get; set;}

        [Column("sold_price")]
        public decimal SoldPrice{get; set;}
        [Column("glasses_id")]
        public Guid GlassesId{get; set;}
        [Column("GlassesId")]
        public Glasses? Glasses{get; set;}

    }
}