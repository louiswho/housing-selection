using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Selection.Context.Selection
{
    public class FilterFactories
    {
        public static GenderFilter GenderFilterFactory()
        {
            return new GenderFilter();
        }

        public static LocationFilter LocationFilterFactory()
        {
            return new LocationFilter();
        }

        public static BatchFilter BatchFilterFactory()
        {
            return new BatchFilter();
        }

        public static HasBedAvailableFilter BedAvailableFilterFactory()
        {
            return new HasBedAvailableFilter();
        }

        public static IsCompletelyUnassignedFilter UnassignedFilterFactory()
        {
            return new IsCompletelyUnassignedFilter();
        }

        public static ARoomFilter ResolveAllFilters()
        {
            GenderFilter gender = GenderFilterFactory();
            LocationFilter location = LocationFilterFactory();
            BatchFilter batch = BatchFilterFactory();
            HasBedAvailableFilter bedAvailable = BedAvailableFilterFactory();
            IsCompletelyUnassignedFilter unassigned = UnassignedFilterFactory();

            gender.SetSuccessor(location);
            location.SetSuccessor(batch);
            batch.SetSuccessor(bedAvailable);
            bedAvailable.SetSuccessor(unassigned);

            return gender;
        }
    }
}
