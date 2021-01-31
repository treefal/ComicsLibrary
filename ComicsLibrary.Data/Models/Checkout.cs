using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.Data.Models
{
    public class Checkout
    {
        public int Id { get; set; }

        [Required]
        public Comic Comic { get; set; }
        public LibraryCard LibraryCard { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}