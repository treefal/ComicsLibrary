using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Customer
{
    public class CustomerListViewModel
    {
        public IEnumerable<CustomerDetailViewModel> Customers { get; set; }
    }
}
