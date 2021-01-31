using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Data.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public virtual Comic Comic { get; set; }
        public virtual LibraryCard LibraryCard { get; set; }
        public DateTime LoanPlaced { get; set; }
    }
}
