﻿using Codes.Data.Configuration;
using Codes.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codes.Data.Context
{
    public class CodesContext : DbContext
    {
        public CodesContext(DbContextOptions<CodesContext> options) : base(options)
        {
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerCategory> CustomerCategories { get; set; }
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
        public virtual DbSet<TrackPrice> TrackPrices { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<TaxCategory> TaxCategories { get; set; }
        public virtual DbSet<TaxType> TaxTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new RepresentativeConfiguration());
            modelBuilder.ApplyConfiguration(new RentConfiguration());
        }
    }
}