using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class CreateExpenseRequest
    {
        [Required(ErrorMessage = "Tutar alanı zorunludur!")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Tutar 0'dan büyük olmalıdır!")]
        public decimal Amount {get; set;}

        [MaxLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir")]
        public string? Description {get; set;}

        [Required(ErrorMessage = "Gider türü seçilmelidir!")]
        public ExpensesType Type {get; set;}
    }
}