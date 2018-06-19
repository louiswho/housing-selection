namespace Housing.Selection.Context.Selection
{
    class UserFilterFactory
    {
        public static UserGenderFilter ResolveUserGenderFilter()
        {
            return new UserGenderFilter();
        }

        public static UserLocationFilter ResolveUserLocationFilter()
        {
            return new UserLocationFilter();
        }

        public static UserBatchFilter ResolveUserBatchFilter()
        {
            return new UserBatchFilter();
        }

        public static UserAssignedFilter ResolveUserAssignedFilter()
        {
            return new UserAssignedFilter();
        }

        public static AUserFilter ResolveUserFilters()
        {
            UserGenderFilter userGender = ResolveUserGenderFilter();
            UserLocationFilter userLocation = ResolveUserLocationFilter();
            UserBatchFilter userBatch = ResolveUserBatchFilter();
            UserAssignedFilter userAssigned = ResolveUserAssignedFilter();

            userGender.SetSuccessor(userLocation);
            userLocation.SetSuccessor(userBatch);
            userBatch.SetSuccessor(userAssigned);

            return userGender;
        }
    }
}
