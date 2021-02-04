using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Library
{
    public class LibraryListViewModel
    {
        public IEnumerable<LibraryDetailViewModel> Libraries { get; set; }
    }
}
