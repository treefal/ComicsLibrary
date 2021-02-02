using ComicsLibrary.Models.Inventory;
using ComicsLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsLibrary.Controllers
{
    public class InventoryController : Controller
    {
        private IComicService _comics;

        public InventoryController(IComicService comics)
        {
            _comics = comics;
        }

        public IActionResult Index()
        {
            var comics = _comics.GetAll();

            var comicsList = comics.Select(result => new ComicViewModel
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl,
                // if errors occur, try calling via service
                Title = result.Title,
                Issue = result.Issue,
                Publisher = result.Publisher,
                Editor = result.Editor,
                GCIN = _comics.GetGcin(result.Id),
                NumberOfCopies = result.NumberOfCopies
            });

            var model = new ComicListViewModel()
            {
                Comics = comicsList
            };

            return View(model);
        }
    }
}
