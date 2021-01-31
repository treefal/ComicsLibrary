using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.Data.Models
{
    public class LibraryLocation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Limit library name to 30 characters.")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }

        public virtual IEnumerable<Customer> Customers { get; set; }
        public virtual IEnumerable<Comic> Comics { get; set; }

        public string ImageUrl { get; set; }
    }
}