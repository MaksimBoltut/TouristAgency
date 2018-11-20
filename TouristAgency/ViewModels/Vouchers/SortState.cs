using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Vouchers
{
    public enum SortState
    {
        IdAsc,
        IdDesc,
        DateBeginningAsc,
        DateBeginningDesc,
        ExpirationDateAsc,
        ExpirationDateDesc,
        HotelIDAsc,
        HotelIDDesc,
        TypeRestIDAsc,
        TypeRestIDDesc,
        ClientIDAsc,
        ClientIDDesc,
        EmployeeIDAsc,
        EmployeeIDDesc,
        BookingNoteAsc,
        BookingNoteDesc,
        PaymentNoteAsc,
        PaymentNoteDesc
    }
}
