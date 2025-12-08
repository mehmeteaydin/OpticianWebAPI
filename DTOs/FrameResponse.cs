using System;

namespace OpticianWebAPI.DTOs
{
    public class FrameResponse
    {
        public Guid Id { get; init; }
        public string? Brand { get; init; }
        public string? ModelCode { get; init; }
        public decimal Cost{get;set;}
        public string? Color { get; init; }
        public string? Material { get; init; }
        public int StockQuantity { get; init; }
    }
}