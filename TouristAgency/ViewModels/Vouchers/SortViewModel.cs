using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Vouchers
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState DateBeginningSort { get; private set; }
        public SortState ExpirationDateSort { get; private set; }
        public SortState HotelIDSort { get; private set; }
        public SortState TypeRestIDSort { get; private set; }
        public SortState ClientIDSort { get; private set; }
        public SortState EmployeeIDSort { get; private set; }
        public SortState BookingNoteSort { get; private set; }
        public SortState PaymentNoteSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            DateBeginningSort = sortOrder == SortState.DateBeginningAsc ? SortState.DateBeginningDesc : SortState.DateBeginningAsc;
            ExpirationDateSort = sortOrder == SortState.ExpirationDateAsc ? SortState.ExpirationDateDesc : SortState.ExpirationDateAsc;
            HotelIDSort = sortOrder == SortState.HotelIDAsc ? SortState.HotelIDDesc : SortState.HotelIDAsc;
            TypeRestIDSort = sortOrder == SortState.TypeRestIDAsc ? SortState.TypeRestIDDesc : SortState.TypeRestIDAsc;
            ClientIDSort = sortOrder == SortState.ClientIDAsc ? SortState.ClientIDDesc : SortState.ClientIDAsc;
            EmployeeIDSort = sortOrder == SortState.EmployeeIDAsc ? SortState.EmployeeIDDesc : SortState.EmployeeIDAsc;
            BookingNoteSort = sortOrder == SortState.BookingNoteAsc ? SortState.BookingNoteDesc : SortState.BookingNoteAsc;
            PaymentNoteSort = sortOrder == SortState.PaymentNoteAsc ? SortState.PaymentNoteDesc : SortState.PaymentNoteAsc;

            Current = sortOrder;
        }
    }
}
