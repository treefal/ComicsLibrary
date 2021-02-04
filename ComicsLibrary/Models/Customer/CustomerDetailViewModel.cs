﻿using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Customer
{
    public class CustomerDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public int LibraryCardId { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string Telephone { get; set; }
        public string HomeLibraryLocation { get; set; }
        public decimal OverdueFees { get; set; }
        public IEnumerable<Checkout> ComicsCheckedOut { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
