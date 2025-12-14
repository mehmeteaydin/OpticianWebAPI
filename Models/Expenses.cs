using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.Models
{
    [Table("expenses")]
   public class Expenses
    {
        [Key]
        [Column("id")]
        public Guid Id{get; set;}

        [Required]
        [Column("amount")]
        public decimal Amount{get; set;}

        [Column("description")]
        [MaxLength(200)]
        public string? Description{get; set;}

        [Column("expense_date")]
        public DateTimeOffset ExpenseDate{get; set;}

        [Column("expense_type")]
        public ExpensesType ExpenseType {get; set;}
    }

    public enum ExpensesType
    {
        Rent = 1,
        Electricity = 2,
        Water = 3,
        NaturalGas = 4,
        Internet = 5,
        Salary = 6,
        Tax = 7,
        Other = 99
    }
}