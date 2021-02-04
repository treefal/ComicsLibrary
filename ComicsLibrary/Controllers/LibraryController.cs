using ComicsLibrary.Models.Library;
using ComicsLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Controllers
{
    public class LibraryController : Controller
    {
        private ILibraryLocationService _libraries;

        public LibraryController(ILibraryLocationService libraries)
        {
            _libraries = libraries;
        }

        public IActionResult Index()
        {
            var allLibraries = _libraries.GetAll()
                .Select(library => new LibraryDetailViewModel
                {
                    Id = library.Id,
                    Name = library.Name,
                    IsOpen = _libraries.IsLocationOpen(library.Id),
                    NumberOfComics = _libraries.GetComics(library.Id).Count(),
                    NumberOfCustomers = _libraries.GetCustomers(library.Id).Count()
                });

            var model = new LibraryListViewModel
            {
                Libraries = allLibraries
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var library = _libraries.Get(id);

            var model = new LibraryDetailViewModel
            {
                Id = library.Id,
                Name = library.Name,
                Address = library.Address,
                Telephone = library.Telephone,
                Description = library.Description,
                OpenDate = library.OpenDate.ToString("dd-MM-yyyy"),
                IsOpen = _libraries.IsLocationOpen(id),
                NumberOfCustomers = _libraries.GetCustomers(id).Count(),
                NumberOfComics = _libraries.GetComics(id).Count(),
                TotalComicsValue = _libraries.GetComics(id).Sum(c => c.Cost),
                ImageUrl = library.ImageUrl,
                HoursOpen = _libraries.GetWorkingHours(id)
            };

            return View(model);
        }
    }
}
