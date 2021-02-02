using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Inventory
{
    public class ComicViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Issue { get; set; }
        public string Publisher { get; set; }
        public string Editor { get; set; }
        public string GCIN { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
