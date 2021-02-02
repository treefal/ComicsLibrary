using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Models.Inventory
{
    public class ComicListViewModel
    {
        public IEnumerable<ComicViewModel> Comics { get; set; }
    }
}
