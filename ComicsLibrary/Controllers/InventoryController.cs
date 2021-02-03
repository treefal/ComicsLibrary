using ComicsLibrary.Models.CheckoutModel;
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
        private ICheckoutService _checkouts;

        public InventoryController(IComicService comics, ICheckoutService checkouts)
        {
            _comics = comics;
            _checkouts = checkouts;
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

            var currentLoans = _checkouts.GetCurrentLoans(id)
                .Select(c => new ComicLoanModel
                {
                    CustomerName = _checkouts.GetCurrentLoanCustomerName(c.Id),
                    LoanPlaced = _checkouts.GetCurrentLoanPlaced(c.Id).ToString("d")
                });

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
                Location = _comics.GetComicLocation(id).Name,
                CustomerName = _checkouts.GetCurrentCheckoutCustomer(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                CurrentLoans = currentLoans
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var comic = _comics.GetById(id);

            var model = new CheckoutViewModel
            {
                ComicId = id,
                ImageUrl = comic.ImageUrl,
                Title = comic.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id });
        }

        public IActionResult Loan(int id)
        {
            var comic = _comics.GetById(id);

            var model = new CheckoutViewModel
            {
                ComicId = id,
                ImageUrl = comic.ImageUrl,
                Title = comic.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                LoanCount = _checkouts.GetCurrentLoans(id).Count()
            };

            return View(model);
        }

        public IActionResult MarkLost(int comicId)
        {
            _checkouts.MarkLost(comicId);
            return RedirectToAction("Detail", new { id = comicId });
        }

        public IActionResult MarkFound(int comicId)
        {
            _checkouts.MarkFound(comicId);
            return RedirectToAction("Detail", new { id = comicId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int comicId, int libraryCardId)
        {
            _checkouts.CheckOutItem(comicId, libraryCardId);
            // we get to the site .../Detail/comicId
            return RedirectToAction("Detail", new { id = comicId });
        }

        [HttpPost]
        public IActionResult PlaceLoan(int comicId, int libraryCardId)
        {
            _checkouts.PlaceLoan(comicId, libraryCardId);
            // we get to the site .../Detail/comicId
            return RedirectToAction("Detail", new { id = comicId });
        }
    }
}
