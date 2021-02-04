using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Services.Interfaces
{
    public interface ILibraryLocationService
    {
        IEnumerable<LibraryLocation> GetAll();
        IEnumerable<Customer> GetCustomers(int locationId);
        IEnumerable<Comic> GetComics(int locationId);
        IEnumerable<string> GetWorkingHours(int locationId);

        LibraryLocation Get(int locationId);
        void Add(LibraryLocation newLocation);
        bool IsLocationOpen(int locationId);
    }
}
