using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer Get(int id);
        IEnumerable<Customer> GetAll();
        void Add(Customer newCustomer);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int customerId);
        IEnumerable<Loan> GetLoans(int customerId);
        IEnumerable<Checkout> GetCheckouts(int customerId);
    }
}
