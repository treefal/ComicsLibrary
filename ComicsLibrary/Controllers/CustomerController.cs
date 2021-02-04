using ComicsLibrary.Data.Models;
using ComicsLibrary.Models.Customer;
using ComicsLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerService _customers;

        public CustomerController(ICustomerService customers)
        {
            _customers = customers;
        }

        public IActionResult Index()
        {
            var allCustomers = _customers.GetAll();

            var customerModels = allCustomers.Select(c => new CustomerDetailViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                LibraryCardId = c.LibraryCard.Id,
                OverdueFees = c.LibraryCard.Fees,
                HomeLibraryLocation = c.HomeLibraryLocation.Name
            }).ToList();

            var model = new CustomerListViewModel
            {
                Customers = customerModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var customer = _customers.Get(id);

            var model = new CustomerDetailViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                LibraryCardId = customer.LibraryCard.Id,
                Address = customer.Address,
                MemberSince = customer.LibraryCard.Created,
                Telephone = customer.TelephoneNumber,
                HomeLibraryLocation = customer.HomeLibraryLocation.Name,
                OverdueFees = customer.LibraryCard.Fees,
                ComicsCheckedOut = _customers.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _customers.GetCheckoutHistory(id),
                Loans = _customers.GetLoans(id)
            };

            return View(model);
        }
    }
}
