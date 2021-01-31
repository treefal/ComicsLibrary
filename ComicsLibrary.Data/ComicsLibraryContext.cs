using ComicsLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Data
{
    public class ComicsLibraryContext : DbContext
    {
        public ComicsLibraryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutHistory> CheckoutsHistories { get; set; }
        public DbSet<LibraryLocation> LibraryLocations { get; set; }
        public DbSet<LibraryLocationHours> LibraryLocationHours { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
