using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.DTOs
{
    public class ExpenseResponse
    {
        public Guid Id {get; set;}
        public decimal Amount {get; set;}
        public string? Description {get; set;}
        public DateTimeOffset ExpenseDate {get; set;}

        public string TypeName {get; set;}
        public int TypeId {get; set;}
    }
}