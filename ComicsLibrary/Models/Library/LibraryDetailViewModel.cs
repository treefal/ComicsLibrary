using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Library
{
    public class LibraryDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public string OpenDate { get; set; }
        public bool IsOpen { get; set; }
        public int NumberOfCustomers { get; set; }
        public int NumberOfComics { get; set; }
        public decimal TotalComicsValue { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<string> HoursOpen { get; set; }
    }
}
