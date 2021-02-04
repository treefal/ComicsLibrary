using ComicsLibrary.Data;
using ComicsLibrary.Data.Models;
using ComicsLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicsLibrary.Services
{
    public class CustomerService : ICustomerService
    {
        private ComicsLibraryContext _context;

        public CustomerService(ComicsLibraryContext context)
        {
            _context = context;
        }

        public void Add(Customer newCustomer)
        {
            _context.Add(newCustomer);
            _context.SaveChanges();
        }

        public Customer Get(int id)
        {
            return GetAll().FirstOrDefault(customer => customer.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers
                .Include(customer => customer.LibraryCard)
                .Include(customer => customer.HomeLibraryLocation);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int customerId)
        {
            var cardId = Get(customerId).LibraryCard.Id;

            return _context.CheckoutsHistories
                .Include(ch => ch.LibraryCard)
                .Include(ch => ch.Comic)
                .Where(ch => ch.LibraryCard.Id == cardId)
                .OrderByDescending(ch => ch.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int customerId)
        {
            var cardId = Get(customerId).LibraryCard.Id;

            return _context.Checkouts
                .Include(c => c.LibraryCard)
                .Include(c => c.Comic)
                .Where(c => c.LibraryCard.Id == cardId);
        }

        public IEnumerable<Loan> GetLoans(int customerId)
        {
            var cardId = Get(customerId).LibraryCard.Id;

            return _context.Loans
                .Include(l => l.LibraryCard)
                .Include(l => l.Comic)
                .Where(l => l.LibraryCard.Id == cardId)
                .OrderByDescending(l => l.LoanPlaced);
        }
    }
}
