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
    public class CheckoutService : ICheckoutService
    {
        private ComicsLibraryContext _context;

        public CheckoutService(ComicsLibraryContext context)
        {
            _context = context;
        }

        public void Add(Checkout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _context.Checkouts;
        }

        public Checkout GetById(int checkoutId)
        {
            return GetAll().FirstOrDefault(checkout => checkout.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int comicId)
        {
            return _context.CheckoutsHistories
                .Include(history => history.Comic)
                .Include(history => history.LibraryCard)
                .Where(history => history.Comic.Id == comicId);
        }

        public IEnumerable<Loan> GetCurrentLoans(int comicId)
        {
            return _context.Loans
                .Include(loan => loan.Comic)
                .Where(loan => loan.Comic.Id == comicId);
        }

        public Checkout GetLatestCheckout(int comicId)
        {
            return _context.Checkouts
                .Where(checkout => checkout.Comic.Id == comicId)
                .OrderByDescending(checkout => checkout.Since)
                .FirstOrDefault();
        }

        public void MarkFound(int comicId)
        {
            var now = DateTime.Now;

            UpdateComicStatus(comicId, "Available");
            RemoveExistingCheckouts(comicId);
            CloseExistingCheckoutHistory(comicId, now);

            _context.SaveChanges();
        }

        private void UpdateComicStatus(int comicId, string status)
        {
            var item = _context.Comics
                .FirstOrDefault(a => a.Id == comicId);

            _context.Update(item); // signals an update to the database

            item.Status = _context.Statuses
                .FirstOrDefault(s => s.Name == status);
        }

        private void CloseExistingCheckoutHistory(int comicId, DateTime now)
        {
            // close any existing checkout history
            var history = _context.CheckoutsHistories
                .FirstOrDefault(h => h.Comic.Id == comicId && h.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
        }

        private void RemoveExistingCheckouts(int comicId)
        {
            // remove any existing checkouts on the item
            var checkout = _context.Checkouts
                .FirstOrDefault(co => co.Comic.Id == comicId);

            if (checkout != null)
            {
                _context.Remove(checkout);
            }
        }

        public void MarkLost(int comicId)
        {
            UpdateComicStatus(comicId, "Lost");
            _context.SaveChanges();
        }

        public void CheckInItem(int comicId)
        {
            var now = DateTime.Now;

            var comic = _context.Comics.FirstOrDefault(c => c.Id == comicId);

            // remove any existing checkouts on the item
            RemoveExistingCheckouts(comicId);

            // close any existing checkout history
            CloseExistingCheckoutHistory(comicId, now);

            // look for existing loans on the item
            var currentLoans = _context.Loans
                .Include(loan => loan.Comic)
                .Include(loan => loan.LibraryCard)
                .Where(loan => loan.Comic.Id == comicId);

            // if loans are placed, checkout item to the
            // library card with earliest loan placed
            if (currentLoans.Any())
            {
                CheckoutToEarliestLoan(comicId, currentLoans);
                return;
            }

            // otherwise, update item status to available
            UpdateComicStatus(comicId, "Available");

            _context.SaveChanges();
        }

        private void CheckoutToEarliestLoan(int comicId, IQueryable<Loan> currentLoans)
        {
            var earliestLoan = currentLoans
                .OrderBy(loans => loans.LoanPlaced)
                .FirstOrDefault();

            var card = earliestLoan.LibraryCard;

            _context.Remove(earliestLoan);
            _context.SaveChanges();
            CheckOutItem(comicId, card.Id);
        }

        public void CheckOutItem(int comicId, int libraryCardId)
        {
            if (IsCheckedOut(comicId))
            {
                return;
                // Add logic for feedback
            }

            var comic = _context.Comics
                .FirstOrDefault(c => c.Id == comicId);

            UpdateComicStatus(comicId, "Checked Out");

            var libraryCard = _context.LibraryCards
                .Include(card => card.Checkouts)
                .FirstOrDefault(card => card.Id == libraryCardId);

            var now = DateTime.Now;

            var checkout = new Checkout
            {
                Comic = comic,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = now,
                Comic = comic,
                LibraryCard = libraryCard
            };

            _context.Add(checkoutHistory);

            _context.SaveChanges();
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int comicId)
        {
            return _context.Checkouts
                .Where(co => co.Comic.Id == comicId)
                .Any();
        }

        public void PlaceLoan(int comicId, int libraryCardId)
        {
            var now = DateTime.Now;

            var comic = _context.Comics
                .Include(c => c.Status)
                .FirstOrDefault(c => c.Id == comicId);

            var card = _context.LibraryCards
                .FirstOrDefault(c => c.Id == libraryCardId);

            if (comic.Status.Name == "Available")
            {
                UpdateComicStatus(comicId, "On Hold");
            }

            var loan = new Loan
            {
                LoanPlaced = now,
                Comic = comic,
                LibraryCard = card
            };

            _context.Add(loan);
            _context.SaveChanges();
        }

        public string GetCurrentLoanCustomerName(int loanId)
        {
            var loan = _context.Loans
                .Include(l => l.Comic)
                .Include(l => l.LibraryCard)
                .FirstOrDefault(l => l.Id == loanId);

            var cardId = loan?.LibraryCard.Id;

            var customer = _context.Customers
                .Include(c => c.LibraryCard)
                .FirstOrDefault(c => c.LibraryCard.Id == cardId);

            return customer?.FirstName + " " + customer?.LastName;
        }

        public DateTime GetCurrentLoanPlaced(int loanId)
        {
            return _context.Loans
                .Include(l => l.Comic)
                .Include(l => l.LibraryCard)
                .FirstOrDefault(l => l.Id == loanId)
                .LoanPlaced;
        }

        public string GetCurrentCheckoutCustomer(int comicId)
        {
            var checkout = GetCheckoutByComicId(comicId);

            if (checkout == null)
            {
                return "";
            }

            var cardId = checkout.LibraryCard.Id;

            var customer = _context.Customers
                .Include(c => c.LibraryCard)
                .FirstOrDefault(c => c.LibraryCard.Id == cardId);

            return customer.FirstName + " " + customer.LastName;
        }

        private Checkout GetCheckoutByComicId(int comicId)
        {
            return _context.Checkouts
                .Include(co => co.Comic)
                .Include(co => co.LibraryCard)
                .FirstOrDefault(co => co.Comic.Id == comicId);
        }
    }
}
