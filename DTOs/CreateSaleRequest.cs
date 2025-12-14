using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class CreateSaleRequest
    {
        public string CustomerName{get; set;}
        public string CustomerPhone{get; set;}

        public Guid FrameId {get; set;}
        
        public string LensBrand{get; set;}
        public double LensDegreeLeft {get; set;}
        public double LensDegreeRight {get; set;}
        public decimal LensCost {get; set;}

        public GlassesType GlassesType{get; set;}
        public string? Description{get; set;}
    }
}