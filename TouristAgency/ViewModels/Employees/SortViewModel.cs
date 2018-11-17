using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Employees
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState FullNameSort { get; private set; }
        public SortState PositionSort { get; private set; }
        public SortState AgeSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            FullNameSort = sortOrder == SortState.FullNameAsc ? SortState.FullNameDesc : SortState.FullNameAsc;
            PositionSort = sortOrder == SortState.PositionAsc ? SortState.PositionDesc : SortState.PositionAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;

            Current = sortOrder;
        }
    }
}
