using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsLibrary.Data.Models
{
    public class LibraryLocationHours
    {
        public int Id { get; set; }
        public LibraryLocation Location { get; set; }

        [Range(1, 7)]
        public int DayOfWeek { get; set; }

        [Range(0, 23)]
        public int OpenTime { get; set; }

        [Range(0, 23)]
        public int CloseTime { get; set; }
    }
}
