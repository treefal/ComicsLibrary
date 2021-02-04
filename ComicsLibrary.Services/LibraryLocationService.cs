using ComicsLibrary.Data;
using ComicsLibrary.Data.Models;
using ComicsLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicsLibrary.Services
{
    public class LibraryLocationService : ILibraryLocationService
    {
        private ComicsLibraryContext _context;

        public LibraryLocationService(ComicsLibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryLocation newLocation)
        {
            _context.Add(newLocation);
            _context.SaveChanges();
        }

        public LibraryLocation Get(int locationId)
        {
            return GetAll().FirstOrDefault(l => l.Id == locationId);
        }

        public IEnumerable<LibraryLocation> GetAll()
        {
            return _context.LibraryLocations
                .Include(l => l.Customers)
                .Include(l => l.Comics);
        }

        public IEnumerable<Comic> GetComics(int locationId)
        {
            return _context.LibraryLocations
                .Include(l => l.Comics)
                .FirstOrDefault(l => l.Id == locationId)
                .Comics;
        }

        public IEnumerable<Customer> GetCustomers(int locationId)
        {
            return _context.LibraryLocations
                .Include(l => l.Customers)
                .FirstOrDefault(l => l.Id == locationId)
                .Customers;
        }

        public IEnumerable<string> GetWorkingHours(int locationId)
        {
            var hours = _context.LibraryLocationHours
                .Where(h => h.Location.Id == locationId);

            return Helpers.HumanizeHours(hours);
        }

        public bool IsLocationOpen(int locationId)
        {
            var currentTimeHour = DateTime.Now.Hour;
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

            var hours = _context.LibraryLocationHours
                .Where(h => h.Location.Id == locationId);
            var daysHours = hours
                .FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);

            return currentTimeHour < daysHours.CloseTime 
                && currentTimeHour > daysHours.OpenTime;
        }
    }
}
