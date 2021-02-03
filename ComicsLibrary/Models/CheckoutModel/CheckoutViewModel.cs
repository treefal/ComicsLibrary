using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.CheckoutModel
{
    public class CheckoutViewModel
    {
        public string LibraryCardId { get; set; }
        public string Title { get; set; }
        public int ComicId { get; set; }
        public string ImageUrl { get; set; }
        public int LoanCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
