using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpticianWebAPI.Models;

namespace OpticianWebAPI.DatabaseContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Gözlük çerçeve için tablo
        public DbSet<Frame> Frames {get; set;}

        // Gözlük camı için tablo
        public DbSet<Lens> Lens {get; set;}

        // Gözlük için tablo
        public DbSet<Glasses> Glasses {get; set;}

        // Gözlük satışı için gerekli olan tablo
        public DbSet<Sales> Sales {get; set;}

        // Giderler için gerekli tablo
        public DbSet<Expenses> Expenses{get; set;}

        
    }
}