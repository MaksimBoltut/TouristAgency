using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Hotels
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState CountrySort { get; private set; }
        public SortState CitySort { get; private set; }
        public SortState StarsSort { get; private set; }
        public SortState ContactFaceSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CountrySort = sortOrder == SortState.CountryAsc ? SortState.CountryDesc : SortState.CountryAsc;
            CitySort = sortOrder == SortState.CityAsc ? SortState.CityDesc : SortState.CityAsc;
            StarsSort = sortOrder == SortState.StarsAsc ? SortState.CityDesc : SortState.StarsAsc;
            ContactFaceSort = sortOrder == SortState.ContactFaceAsc ? SortState.ContactFaceDesc : SortState.ContactFaceAsc;

            Current = sortOrder;
        }
    }
}
