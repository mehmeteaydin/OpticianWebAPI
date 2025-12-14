using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DTOs
{
    public class SaleResponse
    {
        public Guid SaleId{get; set;}
        public string CustomerName {get; set;}
        public DateTimeOffset SaleDate{get; set;}

        public string FrameInfo {get; set;}
        public string LensInfo {get; set;}
        public string GlassesType {get; set;}

        public decimal TotalPrice{get; set;}

     
    }
}