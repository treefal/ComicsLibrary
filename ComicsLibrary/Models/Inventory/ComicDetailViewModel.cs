using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Inventory
{
    public class ComicDetailViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Issue { get; set; }
        
        public string Publisher { get; set; }
        
        public int Year { get; set; }

        public decimal Cost { get; set; }
 
        public string Editor { get; set; }
        
        public string GCIN { get; set; }

        public string Status { get; set; }

        public string ImageUrl { get; set; }

        public int NumberOfCopies { get; set; }

        public string Location { get; set; }

        public string CustomerName { get; set; }

        public Checkout LatestCheckout { get; set; }
        public IEnumerable<CheckoutHistory> CheckoutHistory { get; set; }
        public IEnumerable<ComicLoanModel> CurrentLoans { get; set; }
    }

    public class ComicLoanModel
    {
        public string CustomerName { get; set; }
        public string LoanPlaced { get; set; }
    }
}
