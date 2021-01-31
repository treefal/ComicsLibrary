using System;

namespace ComicsLibrary.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelephoneNumber { get; set; }

        // virtual makes lazy load on the property's data.
        // Lazy loading loads a collection from the database the first time it is accessed.
        public virtual LibraryCard LibraryCard { get; set; }
        public virtual LibraryLocation HomeLibraryLocation { get; set; }
    }
}