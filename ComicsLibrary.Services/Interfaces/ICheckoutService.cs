using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Services.Interfaces
{
    public interface ICheckoutService
    {
        void Add(Checkout newCheckout);

        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int comicId);
        IEnumerable<Loan> GetCurrentLoans(int comicId);

        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int comicId);
        string GetCurrentCheckoutCustomer(int comicId);
        string GetCurrentLoanCustomerName(int loanId);
        DateTime GetCurrentLoanPlaced(int loanId);
        bool IsCheckedOut(int comicId);

        void CheckOutItem(int comicId, int libraryCardId);
        void CheckInItem(int comicId);
        void PlaceLoan(int comicId, int libraryCardId);
        void MarkLost(int comicId);
        void MarkFound(int comicId);
    }
}
