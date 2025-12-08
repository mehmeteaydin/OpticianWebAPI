using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.DTOs
{
    public class LensResponse
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public double Left { get; set; }
        public double Right { get; set; }
        public decimal Cost { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}