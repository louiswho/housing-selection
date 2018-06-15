using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.ViewModels;
using Xunit;

namespace Housing.Selection.Testing.Context
{
    public class ContextFilterTests
    {
        List<Room> TestRooms;
        RoomSearchViewModel SearchModel;

        [Fact]
        public void LocationFilter_Reston_TampaFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                Location = "Reston"
            };
            TestRooms = new List<Room>();

            var room1 = new Room
            {
                Location = "Reston"
            };

            var room2 = new Room
            {
                Location = "Tampa"
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            AFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void LocationFilter_Tampa_RestonFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                Location = "Tampa"
            };
            TestRooms = new List<Room>();

            var room1 = new Room
            {
                Location = "Reston"
            };

            var room2 = new Room
            {
                Location = "Tampa"
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            AFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void BatchFilter_Batch0_FiftyPercent_TestRoom1OnTop()
        {
            SearchModel = new RoomSearchViewModel
            {
                Batch = "Batch0",
                BatchMinimumPercentage = .25
            };
            TestRooms = new List<Room>();

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

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            AFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void BatchFilter_Batch1_OneHundredPercent_AllRoomsFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                Batch = "Batch1",
                BatchMinimumPercentage = 1
            };
            TestRooms = new List<Room>();

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

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            AFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Empty(TestRooms);
        }

        [Fact]
        public void GenderFilter_Female_MaleRoomsFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                Gender = "F"
            };
            TestRooms = new List<Room>();
            AFilter genderFilterTest = new GenderFilter();

            Room room1 = new Room
            {
                Gender = "F"
            };
            Room room2 = new Room
            {
                Gender = "M"
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            genderFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void GenderFilter_Male_FemaleRoomsFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                Gender = "M"
            };
            TestRooms = new List<Room>();
            AFilter genderFilterTest = new GenderFilter();

            Room room1 = new Room
            {
                Gender = "F"
            };
            Room room2 = new Room
            {
                Gender = "M"
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            genderFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_True_FullRoomsFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                HasBedAvailable = true
            };
            TestRooms = new List<Room>();
            AFilter bedAvailableFilterTest = new HasBedAvailableFilter();

            Room room1 = new Room
            {
                Vacancy = 3
            };

            Room room2 = new Room
            {
                Vacancy = 0
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            bedAvailableFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_False_EmptyRoomsFilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                HasBedAvailable = false
            };
            TestRooms = new List<Room>();
            AFilter bedAvailableFilterTest = new HasBedAvailableFilter();

            Room room1 = new Room
            {
                Vacancy = 3
            };

            Room room2 = new Room
            {
                Vacancy = 0
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);

            bedAvailableFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_True_TestRoom4Returned()
        {
            SearchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = true
            };
            TestRooms = new List<Room>();
            AFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

            Room room1 = new Room
            {
                Vacancy = 4,
                Occupancy = 4
            };
            Room room2 = new Room
            {
                Vacancy = 2,
                Occupancy = 4
            };
            Room room3 = new Room
            {
                Vacancy = 0,
                Occupancy = 4
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);
            TestRooms.Add(room3);

            unassignedFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_False_TestRoom4FilteredOut()
        {
            SearchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = false
            };
            TestRooms = new List<Room>();
            AFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

            Room room1 = new Room
            {
                Vacancy = 4,
                Occupancy = 4
            };
            Room room2 = new Room
            {
                Vacancy = 2,
                Occupancy = 4
            };
            Room room3 = new Room
            {
                Vacancy = 0,
                Occupancy = 4
            };

            TestRooms.Add(room1);
            TestRooms.Add(room2);
            TestRooms.Add(room3);

            unassignedFilterTest.FilterRequest(ref TestRooms, SearchModel);

            Assert.Equal(2, TestRooms.Count);
        }
    }
}
