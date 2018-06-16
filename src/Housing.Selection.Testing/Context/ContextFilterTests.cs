using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.ViewModels;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class ContextFilterTests
    {
        private List<Room> _testRooms;
        private RoomSearchViewModel _searchModel;

        [Fact]
        public void LocationFilter_Reston_TampaFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                Location = "Reston"
            };
            _testRooms = new List<Room>();

            var room1 = new Room
            {
                Location = "Reston"
            };

            var room2 = new Room
            {
                Location = "Tampa"
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            AFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void LocationFilter_Tampa_RestonFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                Location = "Tampa"
            };
            _testRooms = new List<Room>();

            var room1 = new Room
            {
                Location = "Reston"
            };

            var room2 = new Room
            {
                Location = "Tampa"
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            AFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void BatchFilter_Batch0_FiftyPercent_TestRoom1OnTop()
        {
            _searchModel = new RoomSearchViewModel
            {
                Batch = "Batch0",
                BatchMinimumPercentage = .25
            };
            _testRooms = new List<Room>();

            Room room1 = new Room
            {
                Vacancy = 1,
                Occupancy = 4
            };
            Room room2 = new Room
            {
                Vacancy = 1,
                Occupancy = 4
            };

            room1.Users = new List<User>();
            room2.Users = new List<User>();

            room1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });
            room1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });

            room2.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch1"
                    }
                });
            room2.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch2"
                    }
                });

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            AFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void BatchFilter_Batch1_OneHundredPercent_AllRoomsFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                Batch = "Batch1",
                BatchMinimumPercentage = 1
            };
            _testRooms = new List<Room>();

            Room room1 = new Room
            {
                Vacancy = 1,
                Occupancy = 4
            };
            Room room2 = new Room
            {
                Vacancy = 1,
                Occupancy = 4
            };

            room1.Users = new List<User>();
            room2.Users = new List<User>();

            room1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });
            room1.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch0"
                    }
                });

            room2.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch1"
                    }
                });
            room2.Users.Add(
                new User
                {
                    Batch = new Batch
                    {
                        BatchName = "Batch2"
                    }
                });

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            AFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Empty(_testRooms);
        }

        [Fact]
        public void GenderFilter_Female_MaleRoomsFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                Gender = "F"
            };
            _testRooms = new List<Room>();
            AFilter genderFilterTest = new GenderFilter();

            Room room1 = new Room
            {
                Gender = "F"
            };
            Room room2 = new Room
            {
                Gender = "M"
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            genderFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void GenderFilter_Male_FemaleRoomsFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                Gender = "M"
            };
            _testRooms = new List<Room>();
            AFilter genderFilterTest = new GenderFilter();

            var room1 = new Room
            {
                Gender = "F"
            };
            var room2 = new Room
            {
                Gender = "M"
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            genderFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_True_FullRoomsFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                HasBedAvailable = true
            };
            _testRooms = new List<Room>();
            AFilter bedAvailableFilterTest = new HasBedAvailableFilter();

            var room1 = new Room
            {
                Vacancy = 3
            };

            var room2 = new Room
            {
                Vacancy = 0
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            bedAvailableFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_False_EmptyRoomsFilteredOut()
        {
            _searchModel = new RoomSearchViewModel
            {
                HasBedAvailable = false
            };
            _testRooms = new List<Room>();
            AFilter bedAvailableFilterTest = new HasBedAvailableFilter();

            var room1 = new Room
            {
                Vacancy = 3
            };

            var room2 = new Room
            {
                Vacancy = 0
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);

            bedAvailableFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_True_TestRoom4Returned()
        {
            _searchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = true
            };
            _testRooms = new List<Room>();
            AFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

            var room1 = new Room
            {
                Vacancy = 4,
                Occupancy = 4
            };
            var room2 = new Room
            {
                Vacancy = 2,
                Occupancy = 4
            };
            var room3 = new Room
            {
                Vacancy = 0,
                Occupancy = 4
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);
            _testRooms.Add(room3);

            unassignedFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Single(_testRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_False_AllRoomsReturned()
        {
            _searchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = false
            };
            _testRooms = new List<Room>();
            AFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

            var room1 = new Room
            {
                Vacancy = 4,
                Occupancy = 4
            };
            var room2 = new Room
            {
                Vacancy = 2,
                Occupancy = 4
            };
            var room3 = new Room
            {
                Vacancy = 0,
                Occupancy = 4
            };

            _testRooms.Add(room1);
            _testRooms.Add(room2);
            _testRooms.Add(room3);

            unassignedFilterTest.FilterRequest(ref _testRooms, _searchModel);

            Assert.Equal(3, _testRooms.Count);
        }
    }
}
