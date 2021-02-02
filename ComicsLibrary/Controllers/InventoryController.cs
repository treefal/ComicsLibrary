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
                Publisher = result.Publisher,
                GCIN = _comics.GetGcin(result.Id)
            });

            var model = new ComicListViewModel()
            {
                Comics = comicsList
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var comic = _comics.GetById(id);

            var model = new ComicDetailViewModel
            {
                Id = id,
                Title = comic.Title,
                Issue = comic.Issue,
                Publisher = comic.Publisher,
                Year = comic.Year,
                Cost = comic.Cost,
                Editor = comic.Editor,
                GCIN = _comics.GetGcin(id),
                Status = comic.Status.Name,
                ImageUrl = comic.ImageUrl,
                NumberOfCopies = comic.NumberOfCopies,
                Location = _comics.GetComicLocation(id).Name
            };

            return View(model);
        }
    }
}
