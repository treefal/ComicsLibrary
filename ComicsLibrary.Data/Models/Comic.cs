using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsLibrary.Data.Models
{
    public class Comic
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Issue { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public string Editor { get; set; }

        [Required]
        public int GCIN { get; set; }

        [Required]
        public Status Status { get; set; }

        public string ImageUrl { get; set; }

        public int NumberOfCopies { get; set; }

        public virtual LibraryLocation Location { get; set; }
    }
}
