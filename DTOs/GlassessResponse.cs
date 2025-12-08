using System;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class GlassessResponse
    {
        public Guid Id { get; set; }
        public Guid FrameId { get; set; }
        public Guid LensId { get; set; }
        public GlassesType Type { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}