using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Clients
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState FullNameSort { get; private set; }
        public SortState DiscountSort { get; private set; }
        public SortState DateBirthSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            FullNameSort = sortOrder == SortState.FullNameAsc ? SortState.FullNameDesc : SortState.FullNameAsc;
            DiscountSort = sortOrder == SortState.DiscountAsc ? SortState.DiscountDesc : SortState.DiscountAsc;
            DateBirthSort = sortOrder == SortState.DateBirthAsc ? SortState.DateBirthDesc : SortState.DateBirthAsc;

            Current = sortOrder;
        }
    }
}
