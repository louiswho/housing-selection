using Housing.Selection.Library.HousingModels;
using System.Collections.Generic;
using Housing.Selection.Context.Selection;
using Housing.Selection.Library.ViewModels;
using Xunit;
using System;

namespace Housing.Selection.Testing.Context
{
    public class ContextFilterTests
    {
        List<Room> TestRooms;
        List<User> TestUsers;
        RoomSearchViewModel RoomSearchModel;
        UserSearchViewModel UserSearchModel;

        [Fact]
        public void LocationFilter_Reston_TampARoomFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
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

            ARoomFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void LocationFilter_Tampa_RestonFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
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

            ARoomFilter locationFilterTest = new LocationFilter();

            locationFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void BatchFilter_Batch0_FiftyPercent_TestRoom1OnTop()
        {
            RoomSearchModel = new RoomSearchViewModel
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

            ARoomFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void BatchFilter_Batch1_OneHundredPercent_AllRoomsFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
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

            ARoomFilter batchFilterTest = new BatchFilter();

            batchFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Empty(TestRooms);
        }

        [Fact]
        public void GenderFilter_Female_MaleRoomsFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                Gender = "F"
            };
            TestRooms = new List<Room>();
            ARoomFilter genderFilterTest = new GenderFilter();

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

            genderFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void GenderFilter_Male_FemaleRoomsFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                Gender = "M"
            };
            TestRooms = new List<Room>();
            ARoomFilter genderFilterTest = new GenderFilter();

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

            genderFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_True_FullRoomsFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                HasBedAvailable = true
            };
            TestRooms = new List<Room>();
            ARoomFilter bedAvailableFilterTest = new HasBedAvailableFilter();

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

            bedAvailableFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void HasBedAvailableFilter_False_EmptyRoomsFilteredOut()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                HasBedAvailable = false
            };
            TestRooms = new List<Room>();
            ARoomFilter bedAvailableFilterTest = new HasBedAvailableFilter();

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

            bedAvailableFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_True_TestRoom4Returned()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = true
            };
            TestRooms = new List<Room>();
            ARoomFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

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

            unassignedFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Single(TestRooms);
        }

        [Fact]
        public void IsCompletelyUnassignedFilter_False_AllRoomsReturned()
        {
            RoomSearchModel = new RoomSearchViewModel
            {
                IsCompletelyUnassigned = false
            };
            TestRooms = new List<Room>();
            ARoomFilter unassignedFilterTest = new IsCompletelyUnassignedFilter();

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

            unassignedFilterTest.FilterRequest(ref TestRooms, RoomSearchModel);

            Assert.Equal(3, TestRooms.Count);
        }

        [Fact]
        public void UserGenderFilter_Female_MaleUsersFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel()
            {
                Gender = "F"
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserGenderFilter();

            User user1 = new User
            {
                Gender = "F"
            };

            User user2 = new User
            {
                Gender = "M"
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserGenderFilter_Male_FemaleUsersFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel()
            {
                Gender = "M"
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserGenderFilter();

            User user1 = new User
            {
                Gender = "F"
            };

            User user2 = new User
            {
                Gender = "M"
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserLocationFilter_Tampa_RestonFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel
            {
                Location = "Tampa"
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserLocationFilter();

            User user1 = new User
            {
                Location = "Tampa"
            };

            User user2 = new User
            {
                Location = "Reston"
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserLocationFilter_Reston_TampaFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel
            {
                Location = "Reston"
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserLocationFilter();

            User user1 = new User
            {
                Location = "Tampa"
            };

            User user2 = new User
            {
                Location = "Reston"
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserBatchFilter_Batch1_Batch2FilteredOut()
        {
            var id = Guid.NewGuid();
            UserSearchModel = new UserSearchViewModel
            {
                Batch = id
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserBatchFilter();

            User user1 = new User
            {
                Batch = new Batch
                {
                    BatchId = id
                }
            };

            User user2 = new User
            {
                Batch = new Batch
                {
                    BatchId = Guid.NewGuid()
                }
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserAssignedFilter_True_UnassignedUsersFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel
            {
                Assigned = true
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserAssignedFilter();

            User user1 = new User
            {
                Room = new Room()
            };

            User user2 = new User
            {
                Room = null
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }

        [Fact]
        public void UserAssignedFilter_False_AssignedUsersFilteredOut()
        {
            UserSearchModel = new UserSearchViewModel
            {
                Assigned = false
            };
            TestUsers = new List<User>();
            AUserFilter userFilterTest = new UserAssignedFilter();

            User user1 = new User
            {
                Room = new Room()
            };

            User user2 = new User
            {
                Room = null
            };

            TestUsers.Add(user1);
            TestUsers.Add(user2);

            userFilterTest.FilterRequest(ref TestUsers, UserSearchModel);

            Assert.Single(TestUsers);
        }
    }
}
