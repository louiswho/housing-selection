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
            var gender = GenderFilterFactory();
            var location = LocationFilterFactory();
            var batch = BatchFilterFactory();
            var bedAvailable = BedAvailableFilterFactory();
            var unassigned = UnassignedFilterFactory();

            gender.SetSuccessor(location);
            location.SetSuccessor(batch);
            batch.SetSuccessor(bedAvailable);
            bedAvailable.SetSuccessor(unassigned);

            return gender;
        }
    }
}
