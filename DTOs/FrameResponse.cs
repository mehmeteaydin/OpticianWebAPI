using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpticianWebAPI.DTOs
{
    public class FrameResponse
    {
        public Guid Id { get; init; }
        public string? Brand { get; init; }
        public string? ModelCode { get; init; }
        public string? Color { get; init; }
        public string? Material { get; init; }
        public int StockQuantity { get; init; }
    }
}