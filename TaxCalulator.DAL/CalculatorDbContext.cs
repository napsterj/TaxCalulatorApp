using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalulator.Entities.Entities;

namespace TaxCalulator.DAL
{
    public class CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Country>().HasData(
                    new Country 
                    { 
                        Id = 1,
                        Name = "Austria"                
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "UK"                     
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Portugal"
                    }
            );
            modelBuilder.Entity<TaxRate>().HasData(
                new TaxRate { Id = 1, CountryId = 1, Rate = 10 },
                new TaxRate { Id = 2, CountryId = 1, Rate = 13 },
                new TaxRate { Id = 3, CountryId = 1, Rate = 20 },

                new TaxRate { Id = 4, CountryId = 2, Rate = 5 },
                new TaxRate { Id = 5, CountryId = 2, Rate = 20 },

                new TaxRate { Id = 6, CountryId = 3, Rate = 6 },
                new TaxRate { Id = 7, CountryId = 3, Rate = 13 },
                new TaxRate { Id = 8, CountryId = 3, Rate = 23 }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<Error> Errors { get; set; }
    }
}
